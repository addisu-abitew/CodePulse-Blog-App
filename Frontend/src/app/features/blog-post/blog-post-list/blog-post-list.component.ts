import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { Blog } from '../models/blog.model';
import { BlogService } from '../services/blog.service';

@Component({
  selector: 'app-blog-post-list',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './blog-post-list.component.html',
  styleUrl: './blog-post-list.component.css'
})
export class BlogPostListComponent {
  blogs$?: Observable<Blog[]>;
  deleteSubscription?: Subscription;
  constructor(private blogService: BlogService, private router: Router) {}

  onDelete(id: string) {
    this.deleteSubscription = this.blogService.deleteBlog(id).subscribe({
      next: () => {
        console.log('Blog Deleted Successfully.');
        window.location.reload();
      },
      error: (err) => {
        console.log('Something went wrong!')
        console.log('Error: ' + err)
      }
    });
  }
  
  ngOnInit(): void {
    this.blogs$ = this.blogService.getBlogs();
  }

  ngOnDestroy(): void {
      this.deleteSubscription?.unsubscribe();
  }
}
