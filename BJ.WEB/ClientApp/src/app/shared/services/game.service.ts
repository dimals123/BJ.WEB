import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { StartGameResponseView } from '../models/game-views/start-game-response-view';
import { GetDetailsGameResponseView } from '../models/game-views/get-details-game-response-view';


@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private http:HttpClient) { }

  

  public stop():void
  {
    this.http.get<void>(environment.BaseUrl + '/Game/Stop').subscribe();
  }

  public getCards():void
  {
    debugger;
    this.http.get<void>(environment.BaseUrl + '/Game/GetCards').subscribe();
  }


  public getDetails():Observable<GetDetailsGameResponseView>
  {
    return this.http.get<GetDetailsGameResponseView>(environment.BaseUrl + '/Game/GetDetails');
  }

  public start(countBots:number):Observable<StartGameResponseView>
  {
    const params = {
      countBots:countBots.toString()
    };
    return this.http.get<StartGameResponseView>(environment.BaseUrl + '/Game/Start', {params:params});
  }

}
