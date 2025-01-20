import { Component, isDevMode } from '@angular/core';
import { Ingredient } from '../../models/ingredient';
import { MOCK_INGREDIENTS } from '../../mocks/ingredients.mock';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatTableModule } from '@angular/material/table';

@Component({
  standalone: true,
  imports: [MatTableModule],
  selector: 'app-ingredients-table',
  templateUrl: './ingredients-table.component.html',
  styleUrls: ['./ingredients-table.component.css'],
  providers: [
    { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} },
  ],
})
export class IngredientsTableComponent {
  ingredients: Ingredient[] = [];
  displayedColumns: string[] = ['id', 'name', 'amount'];

  async ngOnInit() {
    const url = 'http://localhost:5007/ingredients';
    try {
      const response = await fetch(url);
      if (!response.ok) {
        throw new Error(`Response status: ${response.status}`);
      }
    } catch (error) {
      console.error(error);

      if (isDevMode()) {
        // use mock ingredients in dev mode
        // stop at error in production
        this.ingredients = MOCK_INGREDIENTS;
      }
    }
  }
}
