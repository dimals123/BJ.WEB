import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GetAllAccountResponseView } from '../models/account-views/get-all-account-response-view';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { RegisterAccountView } from '../models/account-views/register-account-view';
import { Router } from '@angular/router';
import { LoginAccountResponseView } from '../models/account-views/login-account-response-veiw';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private http:HttpClient, private router: Router) { }
  readonly BaseURI = 'http://localhost:51296';

  ngOnInit()
  {

  }


  register(data:RegisterAccountView) : Observable<LoginAccountResponseView> 
  {
    return this.http.post<LoginAccountResponseView>(this.BaseURI + '/Account/Register', data);
  }

  login(data:RegisterAccountView) : Observable<LoginAccountResponseView>
  {
    return this.http.post<LoginAccountResponseView>(this.BaseURI + '/Account/Login', data);
  }

  getUserId(): Observable<string>
  {
  return this.http.get<string>(this.BaseURI + '/Account/GetUserProfile');
  }

  logout() :void {
    localStorage.removeItem('token');
    localStorage.removeItem('userId');
    localStorage.removeItem('gameId');
    this.router.navigate(['/user/registration']);
  }

  getNames (): Observable<GetAllAccountResponseView> {
    var result = this.http.get<GetAllAccountResponseView>(this.BaseURI + '/Account/GetAll')
    .pipe(
      tap(heroes => console.log('Names is succesful'))
    );
    return result;
  }
  

  public getToken(): string {
    return localStorage.getItem('token');
  }
  public isAuthenticated(): boolean {
    // get the token
    const token = this.getToken();

    const helper = new JwtHelperService();

    // return a boolean reflecting 
    // whether or not the token is expired
    return helper.isTokenExpired(token);
  }

}
