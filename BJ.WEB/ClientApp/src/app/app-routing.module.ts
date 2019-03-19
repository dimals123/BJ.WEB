import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { GameComponent } from './game/game.component';


const routes: Routes = [
  {
    path:'', redirectTo:'/user/registration', pathMatch:'full'
  },
  {
  path:'user', component:UserComponent,
  children:[
    {
      path:'registration', component:RegistrationComponent
    }
  ]
  },
  {path:'game', component:GameComponent, pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
