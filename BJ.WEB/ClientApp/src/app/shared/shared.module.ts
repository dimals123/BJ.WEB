import { NgModule, ModuleWithProviders } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TokenInterceptor } from './interseptors/token-interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AccountService } from './services/account.service';
import { GameService } from './services/game.service';
import { LogOffGuard } from '../guards/log-off.guard';
import { LogInGuard } from '../guards/log-in.guard';
import { LocalStorageService } from './services/local-storage.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './loyaot/menu/menu.component';
import { MatSnackBarModule } from '@angular/material';
import { ErrorHandler } from './interseptors/error-handler';
import { RequestInterceptor } from './interseptors/request-interceptor';


@NgModule({
  declarations: [
    MenuComponent
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports: [
    MatSnackBarModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MenuComponent
  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [AccountService, GameService, LocalStorageService, ErrorHandler,
        {
          provide: HTTP_INTERCEPTORS,
          useClass: RequestInterceptor,
          multi: true,
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: TokenInterceptor,
          multi: true
        },
        LogInGuard,
        LogOffGuard]
    };
  }
}
