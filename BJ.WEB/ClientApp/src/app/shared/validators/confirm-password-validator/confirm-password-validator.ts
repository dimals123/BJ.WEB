import { FormGroup } from '@angular/forms';

export class ConfirmPasswordValidator {

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
