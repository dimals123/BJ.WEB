import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { GetAllAccountResponseView } from '../user/user';
import { Observable, of, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

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
    Password:['', Validators.required]
  });


  register() {
    var body = {
      Name: this.formModel.value.UserName,
      Password: this.formModel.value.Password
    };
    return this.http.post(this.BaseURI + '/Account/Register', body);
  }

  login(formData) {
    return this.http.post(this.BaseURI + '/Account/Login', formData);
  }


  getNames (): Observable<GetAllAccountResponseView> {
    return this.http.get<GetAllAccountResponseView>(this.BaseURI + '/Account/GetAll')
      .pipe(
        tap(heroes => console.log('Names is succesful'))
      );
  }
  

}
