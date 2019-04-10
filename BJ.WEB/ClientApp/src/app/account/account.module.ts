import { NgModule } from '@angular/core';

import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';
import { RegistrationComponent } from './registration/registration.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    AccountComponent,
    RegistrationComponent
  ],
  imports: [
    AccountRoutingModule,
    SharedModule
  ],
  providers: []
})
export class UserModule { }
