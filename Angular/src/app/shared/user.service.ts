import { Injectable } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder) { }


  formModel = this.fb.group({
UserName:['',Validators.required],
Passwords:this.fb.group({
  Password:['',Validators.required, Validators.minLength(4)],
  ConfirmPassword:['',Validators.required]
})
});
}
