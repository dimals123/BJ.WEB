import { Component, OnInit } from '@angular/core';
import { GetUserGamesHistoryView } from '../../models/history-views/get-user-games-history-view';
import { HistoryService } from '../../services/history.service';
import { GetDetailsGameHistoryView } from '../../models/history-views/get-details-game-history-view';
import { PageEvent } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-user-games-expensin-panel',
  templateUrl: './user-games-expensin-panel.component.html',
  styleUrls: ['./user-games-expensin-panel.component.scss']
})
export class UserGamesExpensinPanelComponent implements OnInit {

  private model: GetUserGamesHistoryView;
  private modelGame: GetDetailsGameHistoryView;
  private countGames: number;
  private id: number;
  private imgBot:string;
  private imgHref:any;

  constructor(private readonly historyService: HistoryService, private readonly route: ActivatedRoute, private readonly router: Router) {

  }

  ngOnInit() {
    this.historyService.getUserGames().subscribe(response => {
      this.model = response;
      this.countGames = this.model.games.length;
      this.id = +this.route.snapshot.paramMap.get('id');
    }, error => {
      console.log(error);
    });
    this.imgHref = "../../../../assets/img/"
  }

  public getDetailsGame(gameId: string): void {

    this.historyService.getDetailsGame(gameId).subscribe(response => {
      this.modelGame = response;

    },
      error => {
        console.log(error);
      });
  }


  changeCurrentPage(page): void {
    this.historyService.getUserGames().subscribe(response => {
      this.model = response;
      this.countGames = this.model.games.length;
      this.id = +this.route.snapshot.paramMap.get('id');
    }, error => {
      console.log(error);
    });

    this.router.navigate(['history', page]);
    console.log(page);
  }

}
