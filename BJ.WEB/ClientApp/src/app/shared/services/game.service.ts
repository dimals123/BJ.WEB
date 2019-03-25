import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { StartGameView } from '../models/game-views/start-game-view';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StartGameResponseView } from '../models/game-views/start-game-response-view';
import { CreateStartGameView } from '../models/game-views/create-start-game-view';
import { GetCardsGameView } from '../models/game-views/get-cards-game-view';
import { GetGameIdView } from '../models/game-views/get-game-id-view';


@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http:HttpClient, private router: Router) { }
  readonly BaseURI = 'http://localhost:51296';

  ngOnInit()
  {

  }

  Stop(model:GetCardsGameView):void
  {
    debugger;
    this.http.post<void>(this.BaseURI + '/Game/Stop', model).subscribe(x=>{
      console.log(x);
      window.location.reload();
    });
  }

  getCard(model:GetCardsGameView):void
  {
    debugger;
    this.http.post<void>(this.BaseURI + '/Game/GetCards', model).subscribe(x=>{
      console.log(x);
      window.location.reload();
    });
  }

  getLastGameId(model: CreateStartGameView):Observable<GetGameIdView>
  {
    return this.http.post<GetGameIdView>(this.BaseURI + '/Game/GetGameId', model);
  }

  getGameById(model:GetCardsGameView):Observable<StartGameResponseView>
  {
    return this.http.post<StartGameResponseView>(this.BaseURI + '/Game/GetGameById', model);
  }

  startGame(model:StartGameView):Observable<GetGameIdView>
  {
    return this.http.post<GetGameIdView>(this.BaseURI + '/Game/Start', model);
  }

}
