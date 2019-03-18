import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import {User} from '../user/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private fb:FormBuilder, private http:HttpClient) { }
  readonly BaseURI = 'http://localhost:51296';

  ngOnInit()
  {

  }



  formModel = this.fb.group({
    UserName:['', Validators.required],
    Passwords:this.fb.group({
      Password:['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword:['', Validators.required],  
    }, {validator: this.comparePasswords})
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get('ConfirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (fb.get('Password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      Name: this.formModel.value.UserName,
      Password: this.formModel.value.Passwords.Password
    };
    return this.http.post(this.BaseURI + '/Account/Register', body);
  }

  login() {
    var body = {
      Name: this.formModel.value.UserName,
      Password: this.formModel.value.Passwords.Password,
    };
    return this.http.post(this.BaseURI + '/Account/Login', body);
  }

  getAll()
  {
    var users = this.http.get(this.BaseURI + '/Account/GetAll');
    return users;
  }

}
