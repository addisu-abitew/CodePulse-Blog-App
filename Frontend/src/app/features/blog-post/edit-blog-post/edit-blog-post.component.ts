import { Component, OnDestroy, OnInit } from '@angular/core';
import { UpdateBlogRequest } from '../models/update-blog.model';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { BlogService } from '../services/blog.service';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-edit-blog-post',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-blog-post.component.html',
  styleUrl: './edit-blog-post.component.css',
})
export class EditBlogPostComponent implements OnInit, OnDestroy {
  id: string | null = null;
  blog?: UpdateBlogRequest;
  categories$?: Observable<Category[]>;
  private paramsSubscription?: Subscription;
  private editBlogSubscription?: Subscription;
  constructor(
    private blogService: BlogService,
    private router: Router,
    private route: ActivatedRoute,
    private categoryService: CategoryService
  ) {}

  onFormSubmit() {
    console.log(this.blog);
    this.editBlogSubscription = this.blogService
      .updateBlog(this.id!, this.blog!)
      .subscribe({
        next: (res) => {
          console.log('Blog added successfully.');
          console.log('Response: ' + res);

          this.router.navigateByUrl('/admin/blogs');
        },
        error: (err) => {
          console.log('Something went wrong!');
          console.log('Error: ' + err);
        },
      });
  }

  ngOnInit() {
    this.categories$ = this.categoryService.getCategories();
    this.paramsSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        if (this.id != null) {
          this.blogService.getBlogById(this.id).subscribe({
            next: (res) => {
              this.blog = res;
            },
            error: (err) => {
              console.log('Something went wrong while fetching blog!');
              console.log(err);
            },
          });
        }
      },
    });
  }

  ngOnDestroy(): void {
    this.editBlogSubscription?.unsubscribe();
    this.paramsSubscription?.unsubscribe();
  }
}
