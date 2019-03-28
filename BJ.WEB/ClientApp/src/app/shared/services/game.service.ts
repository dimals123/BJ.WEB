import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StartGameResponseView } from '../models/game-views/start-game-response-view';
import { GetGameIdView } from '../models/game-views/get-game-id-view';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http:HttpClient) { }
  headers={
    headers: new HttpHeaders({
        'Content-Type': 'application/json'
    })
}

  ngOnInit()
  {

  }

  Stop(model:string):void
  {
    debugger;
    let body = JSON.stringify(model);
    this.http.post<void>(environment.BaseUrl + '/Game/Stop', body, this.headers).subscribe(x=>{
      console.log(x);
      window.location.reload();
    });
  }

  getCard(model:string):void
  {
    debugger;
    let body = JSON.stringify(model);
    this.http.post<void>(environment.BaseUrl + '/Game/GetCards', body, this.headers).subscribe(x=>{
      console.log(x);
      window.location.reload();
    });
  }

  getLastGameId():Observable<GetGameIdView>
  {
    return this.http.get<GetGameIdView>(environment.BaseUrl + '/Game/GetLastGame');
  }

  getGameById(model:string):Observable<StartGameResponseView>
  {
    debugger;
    let params1 = new HttpParams().set("gameId", model);
    return this.http.get<StartGameResponseView>(environment.BaseUrl + '/Game/GetGameById', {params: params1});
  }

  startGame(model:number):Observable<GetGameIdView>
  {
    return this.http.post<GetGameIdView>(environment.BaseUrl + '/Game/Start', model);
  }

}
