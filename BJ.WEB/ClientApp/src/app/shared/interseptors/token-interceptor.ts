import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { LocalStorageService } from '../services/local-storage.service';
@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public localStorageService: LocalStorageService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.localStorageService.getItem('token')}`
      }
    });
    return next.handle(request);
  }
}