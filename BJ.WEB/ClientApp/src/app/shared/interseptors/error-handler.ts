import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material';

@Injectable()
export class ErrorHandler {

  constructor(
    public snackbar: MatSnackBar,
  ) {}

  public handleError(err: any) {
    debugger;
    this.snackbar.open(err, 'close');
  }
}