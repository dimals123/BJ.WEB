import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GetDetailsGameResponseView } from '../models/game-views/get-details-game-response-view';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class GameService {

  constructor(private readonly http:HttpClient) { }




  public stop():Observable<void>
  {
    return this.http.get<void>(environment.BaseUrl + '/Game/Stop');
  }

  public getCards():Observable<void>
  {
     return this.http.get<void>(environment.BaseUrl + '/Game/GetCards');
  }

  public getDetails():Observable<GetDetailsGameResponseView>
  {
    return this.http.get<GetDetailsGameResponseView>(environment.BaseUrl + '/Game/GetDetails');
  }

  public start(countBots:number) : Observable<void>
  {
    const params = {
      countBots:countBots.toString()
    };
    return this.http.get<void>(environment.BaseUrl + '/Game/Start', {params:params});
  }

  public isUnfinished() : Observable<boolean>
  {
    return this.http.get<boolean>(environment.BaseUrl + '/Game/IsUnfinished');
  }

}
