import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetAllAccountResponseView } from '../models/account-views/get-all-account-response-view';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'
import { RegisterAccountView } from '../models/account-views/register-account-view';
import { LoginAccountResponseView } from '../models/account-views/login-account-response-veiw';
import { LocalStorageService } from './local-storage.service';
import { LoginAccountView } from '../models/account-views/login-account-view';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private readonly http:HttpClient, private readonly localStorageService:LocalStorageService) { }



  public register(model:RegisterAccountView) : Observable<LoginAccountResponseView> 
  {
    return this.http.post<LoginAccountResponseView>(environment.BaseUrl + '/Account/Register', model);
  }

  public login(model:LoginAccountView) : Observable<LoginAccountResponseView>
  {
    return this.http.post<LoginAccountResponseView>(environment.BaseUrl + '/Account/Login', model);
  }

  public logout() : void {
    this.localStorageService.clear();
    window.location.reload();
  }

  public getAll (): Observable<GetAllAccountResponseView> {
    var result = this.http.get<GetAllAccountResponseView>(environment.BaseUrl + '/Account/GetAll');
    return result;
  }
  

  public getToken(): string {
    return this.localStorageService.getItem<string>('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    return !!token
  }

}
