import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetAllAccountResponseView } from '../models/account-views/get-all-account-response-view';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment'
import { RegisterAccountView } from '../models/account-views/register-account-view';
import { Router } from '@angular/router';
import { LoginAccountResponseView } from '../models/account-views/login-account-response-veiw';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http:HttpClient, private router: Router) { }

  ngOnInit()
  {

  }


  register(data:RegisterAccountView) : Observable<LoginAccountResponseView> 
  {
    return this.http.post<LoginAccountResponseView>(environment.BaseUrl + '/Account/Register', data);
  }

  login(data:RegisterAccountView) : Observable<LoginAccountResponseView>
  {
    return this.http.post<LoginAccountResponseView>(environment.BaseUrl + '/Account/Login', data);
  }

  getUserId(): Observable<string>
  {
  return this.http.get<string>(environment.BaseUrl + '/Account/GetUserProfile');
  }

  logout() :void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('gameId');
    this.router.navigate(['/account/registration']);
  }

  getNames (): Observable<GetAllAccountResponseView> {
    var result = this.http.get<GetAllAccountResponseView>(environment.BaseUrl + '/Account/GetAll');
    return result;
  }
  

  public getToken(): string {
    return localStorage.getItem('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();
    if(token == null)return false;
    else return true;
  }

}
