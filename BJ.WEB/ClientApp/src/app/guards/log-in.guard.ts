import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CanActivate } from '@angular/router/src/utils/preactivation';
import { AccountService } from '../shared/services/account.service';

@Injectable({
  providedIn: 'root'
})

export class LogInGuard implements CanActivate {
  path: ActivatedRouteSnapshot[];
  route: ActivatedRouteSnapshot;

  constructor(private router: Router, private service:AccountService) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (this.service.isAuthenticated() == true)
      return true;
    else {
      this.router.navigate(['/user/registration']);
      return false;
    }

  }
  
}
