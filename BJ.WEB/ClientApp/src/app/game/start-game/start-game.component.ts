import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/shared/services/game.service';
import { StartGameResponseView } from 'src/app/shared/models/game-views/start-game-response-view';
import { GetCardsGameView } from 'src/app/shared/models/game-views/get-cards-game-view';
import { Observable } from 'rxjs';


@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent implements OnInit {

  constructor(private service:GameService, private router: Router) { }

  startGameRespnosne = new StartGameResponseView();
  model = new GetCardsGameView();
  


  ngOnInit() {
  debugger;
    this.model.userId = localStorage.getItem("userId");
    this.model.gameId = localStorage.getItem("gameId");
    this.service.getGameById(this.model).subscribe(data=>this.startGameRespnosne = data);
  }

 

  getCards():void
  {
    debugger;
    this.service.getCard(this.model);
    debugger;
  }

  StopGame()
  {
    this.service.Stop(this.model);
    debugger;
  }
  
  ExitGame()
  {
    this.router.navigateByUrl("/game");
    localStorage.removeItem("gameId");
  }
 

}
