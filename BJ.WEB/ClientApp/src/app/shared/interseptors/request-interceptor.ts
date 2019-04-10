import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
  HttpErrorResponse,
} from '@angular/common/http';
 
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import { ErrorHandler } from './error-handler';
 

 
@Injectable()
export class RequestInterceptor implements HttpInterceptor {
 
  constructor(
    public errorHandler : ErrorHandler,
  ) {}
 
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    return next.handle(request).do((event: HttpEvent<any>) => {}, (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if(err.status == 400)
        this.errorHandler.handleError("Bad Request.");
        else 
        this.errorHandler.handleError("Internal Server Error.");
      }
    });
  }
}