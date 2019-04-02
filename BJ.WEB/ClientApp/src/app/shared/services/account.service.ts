import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetAllAccountResponseView } from '../models/account-views/get-all-account-response-view';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'
import { RegisterAccountView } from '../models/account-views/register-account-view';
import { Router } from '@angular/router';
import { LoginAccountResponseView } from '../models/account-views/login-account-response-veiw';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http:HttpClient, private router: Router, private LocalStorageService:LocalStorageService) { }



  public register(model:RegisterAccountView) : Observable<LoginAccountResponseView> 
  {
    return this.http.post<LoginAccountResponseView>(environment.BaseUrl + '/Account/Register', model);
  }

  public login(model:RegisterAccountView) : Observable<LoginAccountResponseView>
  {
    return this.http.post<LoginAccountResponseView>(environment.BaseUrl + '/Account/Login', model);
  }

  public logout() :void {
    this.LocalStorageService.clear();
  }

  public getAll (): Observable<GetAllAccountResponseView> {
    var result = this.http.get<GetAllAccountResponseView>(environment.BaseUrl + '/Account/GetAll');
    return result;
  }
  

  public getToken(): string {
    return this.LocalStorageService.getItem<string>('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    return token == null ? false : true
  }

}
