import { Component, OnInit } from '@angular/core';
import { AccountService } from '../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {

  constructor(private accountService: AccountService, private router:Router) { }




  
  public onLogout(): void {
    this.accountService.logout();
    this.router.navigate(['/account/registration']);
  }
}
