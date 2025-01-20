import { Component } from '@angular/core';
import { IngredientsTableComponent } from "./ingredients-table/ingredients-table.component";

@Component({
  selector: 'app-root',
  imports: [IngredientsTableComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ng-app';
}
