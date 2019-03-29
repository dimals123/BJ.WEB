import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';



import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LogInGuard } from './guards/log-in.guard';
import { LogOffGuard } from './guards/log-off.guard';
import { SharedModule } from './shared/shared.module';




@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule
  ],
  providers: [
    LogInGuard, 
    LogOffGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
