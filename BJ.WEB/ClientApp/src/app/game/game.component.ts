import { Component, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../shared/services/account.service';
import { FormBuilder, NgForm, Validators } from '@angular/forms';
import { StartGameView } from '../shared/models/game-views/start-game-view';
import { GameService } from '../shared/services/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styles: []
})
export class GameComponent implements OnInit {

  startGame = new StartGameView();
  

  constructor(private serviceAccount: AccountService, private serviceGame: GameService, private router:Router, private formbuilder:FormBuilder) { }


  

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
