import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
  constructor(private snackBar: MatSnackBar) {}

  handle(error: any): void {
    if (error instanceof HttpErrorResponse) {
      const message = error.error?.message || 'Naməlum xəta baş verdi.';
      this.showError(message);
    } else {
      this.showError('Naməlum xəta baş verdi');
    }
  }

  private showError(message: string) {
    this.snackBar.open(message, 'Bağla', {
      duration: 3000,
      panelClass: ['snackbar-error'],
    });
  }
}
