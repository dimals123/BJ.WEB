import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../shared/services/account.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styles: []
})
export class GameComponent {
  

  constructor(private accountService: AccountService, private router:Router) { }

  
 

}
