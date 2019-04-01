import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/shared/services/account.service';
import { GetAllAccountResponseView } from '../../shared/models/account-views/get-all-account-response-view';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { RegisterAccountView } from 'src/app/shared/models/account-views/register-account-view';
import { LoginAccountResponseView } from 'src/app/shared/models/account-views/login-account-response-veiw';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit 
{


  
  constructor(private accountService:AccountService, private router: Router, private formBuilder:FormBuilder, private LocalStorageService:LocalStorageService) { }
  private users = new GetAllAccountResponseView();

  private formModel = this.formBuilder.group({
    name:['', Validators.required],
    password:['', Validators.required]
  });


  public ngOnInit(): void {
   this.accountService.getAll().subscribe(data=>this.users = data);
  }

  private authorize(res:LoginAccountResponseView): void{
    this.LocalStorageService.setItem('token', res.token);
    this.router.navigateByUrl('/game');
  }


  public onSubmit() :void
  {
    var register = new RegisterAccountView();
    register = this.formModel.value; 
      if(this.users.names.indexOf(this.formModel.value.name) != -1)
      {
        this.accountService.login(register).subscribe(
          res => {
            this.authorize(res);
          },
          err => {
              console.log(err);
          });
        }
  else
  {
    this.accountService.register(register).subscribe(
      res => {
        this.authorize(res);
      },
      err => {
        console.log(err);
      });
  } 
  }
}
  


