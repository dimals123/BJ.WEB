import { NgModule, ModuleWithProviders } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TokenInterceptor } from './interseptors/token.interceptor';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AccountService } from './services/account.service';
import { GameService } from './services/game.service';
import { LogOffGuard } from './guards/log-off.guard';
import { LogInGuard } from './guards/log-in.guard';
import { LocalStorageService } from './services/local-storage.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './layout/menu/menu.component';
import { MatSnackBarModule } from '@angular/material';
import { ErrorHandler } from './handlers/error.handler';
import { RequestInterceptor } from './interseptors/request.interceptor';
import { HistoryService } from './services/history.service';
import { MatExpansionModule } from '@angular/material/expansion';
import { LayoutModule } from '@angular/cdk/layout';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NgxPaginationModule } from 'ngx-pagination';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatSidenavModule} from '@angular/material/sidenav';
import { RestrictInputDirective } from './derectives/restrict-input-directive';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';

@NgModule({
  declarations: [
    MenuComponent,
    RestrictInputDirective
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    MatExpansionModule,
    LayoutModule,
    MatPaginatorModule,
    NgxPaginationModule,
    MatIconModule,
    MatMenuModule,
    MatSidenavModule,
    MatProgressSpinnerModule
  ],
  exports: [
    MatSnackBarModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MenuComponent,
    MatExpansionModule,
    LayoutModule,
    MatPaginatorModule,
    NgxPaginationModule,
    MatIconModule,
    MatMenuModule,
    MatSidenavModule,
    RestrictInputDirective,
    MatProgressSpinnerModule

  ]
})
export class SharedModule {
  static forRoot(): ModuleWithProviders {
    return {
      ngModule: SharedModule,
      providers: [
        AccountService,
        GameService,
        LocalStorageService,
        ErrorHandler,
        HistoryService,
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
