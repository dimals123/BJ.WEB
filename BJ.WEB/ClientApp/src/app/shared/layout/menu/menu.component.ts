import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {
  constructor(private readonly accountService: AccountService, private readonly router: Router) {
  }


logout(): void{
  this.accountService.logout();
}

}
