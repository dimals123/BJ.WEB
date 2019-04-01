import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';
import { AccountService } from '../shared/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class LogOffGuard implements CanActivate  {
  
  constructor(private router: Router, private accountService:AccountService) {
  }
  public canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (!this.accountService.isAuthenticated())
      return true;
    else {
      this.router.navigate(['/game']);
      return false;
    }

  }

}
