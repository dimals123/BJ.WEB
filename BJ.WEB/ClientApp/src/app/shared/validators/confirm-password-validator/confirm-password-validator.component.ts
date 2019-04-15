import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-confirm-password-validator',
  templateUrl: './confirm-password-validator.component.html',
  styleUrls: ['./confirm-password-validator.component.scss']
})
export class ConfirmPasswordValidatorComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }


  public comparePasswords(formBuilder: FormGroup) {
    let confirmPasswordControl = formBuilder.get('confirmPassword');
    if (confirmPasswordControl.errors == null || 'passwordMismatch' in confirmPasswordControl.errors) {
      if (formBuilder.get('password').value != confirmPasswordControl.value)
        confirmPasswordControl.setErrors({ passwordMismatch: true });
      else
        confirmPasswordControl.setErrors(null);
    }

  }


}
