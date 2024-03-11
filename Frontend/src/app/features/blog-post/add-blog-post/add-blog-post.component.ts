import { Component } from '@angular/core';
import { AddBlogRequest } from '../models/add-blog-request.model';
import { Observable, Subscription } from 'rxjs';
import { BlogService } from '../services/blog.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MarkdownModule } from 'ngx-markdown';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blog-post',
  standalone: true,
  imports: [CommonModule, FormsModule, MarkdownModule],
  templateUrl: './add-blog-post.component.html',
  styleUrl: './add-blog-post.component.css',
})
export class AddBlogPostComponent {
  model: AddBlogRequest;
  categories$?: Observable<Category[]>;
  private addBlogSubscription?: Subscription;
  constructor(
    private blogService: BlogService,
    private router: Router,
    private categoryService: CategoryService
  ) {
    this.model = {
      title: '',
      urlHandle: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      author: '',
      isVisible: true,
      categories: [],
    };
  }
  onFormSubmit() {
    console.log(this.model);
    this.addBlogSubscription = this.blogService.addBlog(this.model).subscribe({
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
  }

  ngOnDestroy(): void {
    this.addBlogSubscription?.unsubscribe();
  }
}
