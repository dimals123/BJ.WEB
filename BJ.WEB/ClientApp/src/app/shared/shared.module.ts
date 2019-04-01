import { NgModule, ModuleWithProviders } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { LayoutModule } from '@progress/kendo-angular-layout';
import { TokenInterceptor } from './interseptors/token-interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AccountService } from './services/account.service';
import { GameService } from './services/game.service';
import { LogOffGuard } from '../guards/log-off.guard';
import { LogInGuard } from '../guards/log-in.guard';
import { LocalStorageService } from './services/local-storage.service';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [
],

 exports:[
    FormsModule,
    ReactiveFormsModule,
    LayoutModule,
    HttpClientModule,
    RouterModule
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [ AccountService, GameService, LocalStorageService,
        {
        provide: HTTP_INTERCEPTORS,
        useClass: TokenInterceptor,
        multi: true
      },
      LogInGuard, 
      LogOffGuard ]
    };
  }
 }
