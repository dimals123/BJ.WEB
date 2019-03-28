import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/shared/services/game.service';
import { StartGameResponseView } from 'src/app/shared/models/game-views/start-game-response-view';
import { FormBuilder, NgForm, Validators } from '@angular/forms';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent implements OnInit {
  serviceGame: any;

  constructor(private service:GameService, private router: Router, private formbuilder:FormBuilder) { }

  startGameRespnosne = new StartGameResponseView();
  gameId : string;
  countBots : number;
  

  formModel = this.formbuilder.group({
    CountBots:[0, Validators.required],
  });


  ngOnInit() {
    debugger;
    this.gameId = localStorage.getItem("gameId");
    this.service.getGameById(this.gameId).subscribe(data=>this.startGameRespnosne = data);
  }

 

  getCards():void
  {
    this.service.getCard(this.gameId);
    debugger;
  }

  StopGame()
  {
    this.service.Stop(this.gameId);
    debugger;
  }
  
  ExitGame()
  {
    this.router.navigateByUrl("/game");
    localStorage.removeItem("gameId");
  }
 
  onStartGame() {
    debugger;
    this.countBots = this.formModel.value.CountBots;
    this.service.startGame(this.countBots).subscribe(data=>{localStorage.setItem('gameId', data.gameId), window.location.reload();});
  }

}
