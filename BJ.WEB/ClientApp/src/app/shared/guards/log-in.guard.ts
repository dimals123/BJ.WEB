import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CanActivate } from '@angular/router/src/utils/preactivation';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})

export class LogInGuard implements CanActivate {
  path: ActivatedRouteSnapshot[];
  route: ActivatedRouteSnapshot;

  constructor(private router: Router, private accountService:AccountService) {
  }
  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (this.accountService.isAuthenticated())
      return true;
    else {
      this.router.navigate(['/user/registration']);
      return false;
    }

  }
  
}
