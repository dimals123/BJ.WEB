import { NgModule } from '@angular/core';

import { GameRoutingModule } from './game-routing.module';
import { GameComponent } from './game.component';

import { StartGameComponent } from './start-game/start-game.component';
import { SharedModule } from '../shared/shared.module';
import { GamePlayComponent } from './game-play/game-play.component';



@NgModule({
  declarations: [
    GameComponent,
    GamePlayComponent,
    StartGameComponent
  ],
  imports: [
    GameRoutingModule,
    SharedModule
  ],
  providers: []
})
export class GameModule { }
