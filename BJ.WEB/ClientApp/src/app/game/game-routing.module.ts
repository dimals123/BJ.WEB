import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StartGameComponent } from './start-game/start-game.component';
import { GameComponent } from './game.component';


const routes: Routes = [
  {
    path:'',
    component:GameComponent,
    children:[
      {
        path:'start-game',
        component:StartGameComponent
      }
  ]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GameRoutingModule { }
