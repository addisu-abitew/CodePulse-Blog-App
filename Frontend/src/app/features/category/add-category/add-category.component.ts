import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AddCategoryRequest } from '../../../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent implements OnDestroy {
  model: AddCategoryRequest;
  private addCategorySubscription?: Subscription;
  constructor(private categoryService: CategoryService) {
    this.model = {
      name: '',
      urlHandle: ''
    }
  }
  onFormSubmit() {
    console.log(this.model);
    this.addCategorySubscription = this.categoryService.addCategory(this.model).subscribe({
      next: (res) => {
        console.log('Category added successfully.');
        console.log('Response: ' + res);
      },
      error: (err) => {
        console.log('Something went wrong!');
        console.log('Error: ' + err);
      }
    });
  }

  ngOnDestroy(): void {
      this.addCategorySubscription?.unsubscribe();
  }
}
