import { Component, OnInit, OnChanges } from '@angular/core';
import { GetDetailsGameResponseView } from 'src/app/shared/models/game-views/get-details-game-response-view';
import { GameService } from 'src/app/shared/services/game.service';

@Component({
  selector: 'app-game-play',
  templateUrl: './game-play.component.html',
  styleUrls: ['./game-play.component.scss']
})
export class GamePlayComponent implements OnInit, OnChanges {

  constructor(private readonly gameService: GameService) {
  }

  private isStop:boolean;
  private model = new GetDetailsGameResponseView();


  public ngOnChanges(): void {

  }

  public ngOnInit(): void {
    this.gameService.getDetails().subscribe(response => this.model = response);
  }

  public getCards($event): void {
    this.gameService.getCards().subscribe(response => {
      console.log("getCards");
      this.gameService.getDetails().subscribe(response => {
        this.model = response;
      });
    })


  }

  public stopGame($event): void {
    this.gameService.stop().subscribe(response => {
      console.log("stop");
      this.gameService.getDetails().subscribe(response => {
        this.model = response;
      });
    });
    this.isStop = true;

  }
}