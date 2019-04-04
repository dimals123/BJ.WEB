import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInGuard } from './guards/log-in.guard';
import { LogOffGuard } from './guards/log-off.guard';


const routes: Routes = [
  {
    path: '',
    redirectTo: '/account/registration',
    pathMatch: 'full'
  },
  {
    path: 'account',
    loadChildren: './account/account.module#UserModule',
    canActivate: [LogOffGuard]
  },
  {
    path: 'game',
    loadChildren: './game/game.module#GameModule',
    canActivate: [LogInGuard]
  },
  {
    path: 'history',
    loadChildren: './history/history.module#HistoryModule',
    canActivate: [LogInGuard]
  },
  {
    path: '**', redirectTo: '/account/registration'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
