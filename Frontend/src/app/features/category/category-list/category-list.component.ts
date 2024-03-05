import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CategoryService } from '../services/category.service';
import { Category } from '../../../models/category.model';
import { CommonModule } from '@angular/common';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit, OnDestroy {
  categories$?: Observable<Category[]>;
  deleteSubscription?: Subscription;
  constructor(private categoryService: CategoryService, private router: Router) {}

  onDelete(id: string) {
    this.deleteSubscription = this.categoryService.deleteCategory(id).subscribe({
      next: (res) => {
        console.log('Category Deleted Successfully.');
        this.router.navigateByUrl('/admin/categories');
      },
      error: (err) => {
        console.log('Something went wrong!')
        console.log('Error: ' + err)
      }
    });
  }
  
  ngOnInit(): void {
    this.categories$ = this.categoryService.getCategories();
  }

  ngOnDestroy(): void {
      this.deleteSubscription?.unsubscribe();
  }
}
