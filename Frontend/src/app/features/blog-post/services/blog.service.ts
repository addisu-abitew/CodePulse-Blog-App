import { Injectable } from '@angular/core';
import { AddBlogRequest } from '../models/add-blog-request.model';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';
import { Blog } from '../models/blog.model';
import { UpdateBlogRequest } from '../models/update-blog.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(private http: HttpClient) { }

  addBlog(model: AddBlogRequest): Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Blogs`, model);
  }

  getBlogs(): Observable<Blog[]> {
    return this.http.get<Blog[]>(`${environment.apiBaseUrl}/api/Blogs`);
  }

  getBlogById(id: string) {
    return this.http.get<Blog | undefined>(`${environment.apiBaseUrl}/api/Blogs/${id}`);
  }

  updateBlog(id: string, model: UpdateBlogRequest): Observable<Blog> {
    return this.http.put<Blog>(`${environment.apiBaseUrl}/api/Blogs/${id}`, model);
  }

  deleteBlog(id: string): Observable<void> {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/Blogs/${id}`);
  }
}
