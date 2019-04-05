import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetUserGamesHistoryView } from '../models/history-views/get-user-games-history-view';
import { GetDetailsGameHistoryView } from '../models/history-views/get-details-game-history-view';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor(private readonly http: HttpClient) { }




  public getUserGames(): Observable<GetUserGamesHistoryView> {
    return this.http.get<GetUserGamesHistoryView>(environment.BaseUrl + '/History/GetUserGames').pipe(response=>
      {
        return response;
      });
  }

  public getDetailsGame(gameId:string):Observable<GetDetailsGameHistoryView>{
    let params = {
      gameId:gameId
    }
    return this.http.get<GetDetailsGameHistoryView>(environment.BaseUrl + '/History/GetDetailsGame', {params:params});
  }
}
