import { NgModule } from '@angular/core';

import { HistoryRoutingModule } from './history-routing.module';
import { SharedModule } from '../shared/shared.module';
import { HistoryComponent } from './history.component';


@NgModule({
  declarations: [HistoryComponent],
  imports: [
    SharedModule,
    HistoryRoutingModule
  ]
})
export class HistoryModule { }
