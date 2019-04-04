import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './start-game/start-game.component';
import { GameComponent } from './game.component';
import { GamePlayComponent } from './game-play/game-play.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: '/game/start-game',
    pathMatch: 'full'
  },
  {
    path: '',
    component: GameComponent,
    children: [
      {
        path: 'start-game',
        component: StartGameComponent
      },
      {
        path: 'game-play',
        component: GamePlayComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GameRoutingModule { }
