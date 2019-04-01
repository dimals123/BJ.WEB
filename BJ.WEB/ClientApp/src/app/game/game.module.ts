import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GameRoutingModule } from './game-routing.module';
import { GameComponent } from './game.component';

import { StartGameComponent } from './start-game/start-game.component';
import { SharedModule } from '../shared/shared.module';
import { MenuComponent } from '../shared/menu/menu.component';


@NgModule({
  declarations: [
    GameComponent,
    StartGameComponent,
    MenuComponent
  ],
  imports: [
    CommonModule,
    GameRoutingModule,
    SharedModule
    
  ],
  providers: [],
  bootstrap: [GameComponent]
})
export class GameModule { }
