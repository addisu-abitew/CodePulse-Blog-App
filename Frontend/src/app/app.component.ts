import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet } from '@angular/router';
import { NavbarComponent } from "./components/navbar/navbar.component";
import { HttpClientModule } from '@angular/common/http';
import { CategoryService } from './features/category/services/category.service';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [CommonModule, RouterOutlet, NavbarComponent, RouterModule, HttpClientModule],
    providers: [CategoryService]
})
export class AppComponent {
  title = 'CodePulse';
}
