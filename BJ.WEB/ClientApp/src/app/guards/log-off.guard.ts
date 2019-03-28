import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { AccountService } from '../shared/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class LogOffGuard implements CanActivate  {
  
  constructor(private router: Router, private service:AccountService) {
  }
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (this.service.isAuthenticated() == false)
      return true;
    else {
      this.router.navigate(['/game']);
      return false;
    }

  }

}
