import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameRoutingModule } from './game-routing.module';
import { GameComponent } from './game.component';
import { AccountService } from '../shared/services/account.service';

import { StartGameComponent } from './start-game/start-game.component';
import { GameService } from '../shared/services/game.service';


@NgModule({
  declarations: [
    GameComponent,
    StartGameComponent
  ],
  imports: [
    CommonModule,
    GameRoutingModule
   
  ],
  providers: [AccountService, GameService],
  bootstrap: [GameComponent]
})
export class GameModule { }
