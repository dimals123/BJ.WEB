import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { GameService } from 'src/app/shared/services/game.service';
import { FormBuilder, Validators } from '@angular/forms';
import { GetDetailsGameResponseView } from 'src/app/shared/models/game-views/get-details-game-response-view';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';


@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.scss']
})
export class StartGameComponent implements OnInit {

  constructor(private gameService:GameService, private router: Router, private formBuilder:FormBuilder, private LocaleStorageService:LocalStorageService) { }

  private startGameRespnosne = new GetDetailsGameResponseView();

  private formModel = this.formBuilder.group({
    countBots:[0, Validators.required],
  });


  public ngOnInit() {
    this.gameService.getDetails().subscribe(data=>this.startGameRespnosne = data);
  }
  
  public getCards():void
  {
    debugger;
    this.gameService.getCards();
    window.location.reload();
  }

  public StopGame():void
  {
    this.gameService.stop();
    window.location.reload();
    debugger;
  }
  
  public exitGame():void
  {
    this.router.navigateByUrl("/game");
    this.LocaleStorageService.removeItem("gameId");
  }
 
  public onStartGame():void {
    debugger;
    this.gameService.start(this.formModel.value.countBots).subscribe();
    window.location.reload();
  }

}
