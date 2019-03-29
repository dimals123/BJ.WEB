import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { StartGameResponseView } from '../models/game-views/start-game-response-view';
import { GetDetailsGameResponseView } from '../models/game-views/get-details-game-response-view';


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
    let params1 = new HttpParams().set("gameId", model);
    this.http.post<void>(environment.BaseUrl + '/Game/Stop', {params:params1}).subscribe(x=>{
      console.log(x);
      window.location.reload();
    });
  }

  getCard(model:string):void
  {
    debugger;
    let params1 = new HttpParams().set("gameId", model);
    this.http.get<void>(environment.BaseUrl + '/Game/GetCards', {params:params1}).subscribe(x=>{
      console.log(x);
      window.location.reload();
    });
  }


  getDetails(model:string):Observable<GetDetailsGameResponseView>
  {
    let params1 = new HttpParams().set("gameId", model);
    return this.http.get<GetDetailsGameResponseView>(environment.BaseUrl + '/Game/GetDetails', {params: params1});
  }

  startGame(model:number):Observable<StartGameResponseView>
  {
    let params1 = new HttpParams().set("countBots", model.toString());
    return this.http.get<StartGameResponseView>(environment.BaseUrl + '/Game/Start', {params:params1});
  }

}
