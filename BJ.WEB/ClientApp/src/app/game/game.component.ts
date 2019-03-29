import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../shared/services/account.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styles: []
})
export class GameComponent implements OnInit {
  

  constructor(private serviceAccount: AccountService, private router:Router) { }


  

  ngOnInit() {
   
  }

  onLogout(): void {
    this.serviceAccount.logout();
  }

  onStart()
{
  this.router.navigateByUrl("/game/start-game");
}

 

}
