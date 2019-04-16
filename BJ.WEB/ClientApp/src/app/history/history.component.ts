import { Component, OnInit } from '@angular/core';
import { HistoryService } from '../shared/services/history.service';
import { ActivatedRoute, Router } from '@angular/router';
import { GetDetailsGameHistoryView } from '../shared/models/history-views/get-details-game-history-view';
import { GetUserGamesHistoryView } from '../shared/models/history-views/get-user-games-history-view';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  private model: GetUserGamesHistoryView;
  private modelGame: GetDetailsGameHistoryView;
  private countGames: number;
  private id: number;

  constructor(private readonly historyService: HistoryService, private readonly route: ActivatedRoute, private readonly router: Router) {

  }

  ngOnInit() {
    this.id = this.getParamMap();
    this.changeCurrentPage(this.id);
  }

  public getDetailsGame(gameId: string): void {

    this.historyService.getDetailsGame(gameId).subscribe(response => {
      this.modelGame = response;
    },
      error => {
        console.log(error);
      });
  }

  private getParamMap(): number{
    return +this.route.snapshot.paramMap.get('id');
  }

  changeCurrentPage(page): void {
    this.historyService.getUserGames().subscribe(response => {
      this.model = response;
      this.countGames = this.model.games.length;
      this.id = this.getParamMap();
    }, error => {
      console.log(error);
    });

    this.router.navigate(['history', page]);
    console.log(page);
  }

}
