import { Component, importProvidersFrom } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { CategoryService } from './features/category/services/category.service';
import { BlogService } from './features/blog-post/services/blog.service';
import { MarkdownModule, MarkdownService } from 'ngx-markdown';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [
    CommonModule,
    RouterOutlet,
    NavbarComponent,
    RouterModule,
    HttpClientModule,
    MarkdownModule,
  ],
  providers: [CategoryService, BlogService, MarkdownService],
})
export class AppComponent {
  title = 'CodePulse';
}
