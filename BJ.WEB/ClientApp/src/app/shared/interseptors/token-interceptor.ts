import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { AccountService } from '../services/account.service';
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(private readonly accountService: AccountService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      setHeaders: {
        // Authorization: `Bearer ${this.accountService.getToken()}`
         Authorization: `Bearer ${this.accountService.getToken()}`
      }
    });
    return next.handle(request);
  }
}