import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInGuard } from './guards/log-in.guard';
import { LogOffGuard } from './guards/log-off.guard';
import { AppComponent } from './app.component';


const routes: Routes = [
  {
    path:'',
    redirectTo:'/user/registration',
    pathMatch:'full'
  },
  {
    path:'', 
    component:AppComponent,
    children:[
      {
        path:'account', 
        loadChildren:'./account/account.module#UserModule', 
        canActivate:[LogOffGuard]
      },
      {
        path:'game', 
        loadChildren:'./game/game.module#GameModule', 
        canActivate:[LogInGuard]
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
