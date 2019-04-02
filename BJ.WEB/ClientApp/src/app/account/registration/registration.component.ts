import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/shared/services/account.service';
import { GetAllAccountResponseView } from '../../shared/models/account-views/get-all-account-response-view';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RegisterAccountView } from 'src/app/shared/models/account-views/register-account-view';
import { LoginAccountResponseView } from 'src/app/shared/models/account-views/login-account-response-veiw';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LoginAccountView } from 'src/app/shared/models/account-views/login-account-view';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit 
{


  
  constructor(private accountService:AccountService, private router: Router, private formBuilder:FormBuilder, private LocalStorageService:LocalStorageService) { }
  private users = new GetAllAccountResponseView();

  private formModel = this.formBuilder.group({
    name:['', Validators.required],
    passwords:this.formBuilder.group({
      password:['', [Validators.required, Validators.minLength(8)]],
      confirmPassword:['', Validators.required]},
      {validator : this.comparePasswords})
  });


  private comparePasswords(formBuilder:FormGroup)
  {
    let confirmPswrdCtrl = formBuilder.get('confirmPassword');
    //passwordMismatch
    //confirmPswrdCtrl.errors={passwordMismatch:true}
    if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
      if (formBuilder.get('password').value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else
        confirmPswrdCtrl.setErrors(null);
    }

  }

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
    var login = new LoginAccountView();
      if(this.users.names.indexOf(this.formModel.value.name) != -1)
      {
        login = 
        {
          name : this.formModel.value.name,
          password : this.formModel.value.passwords.password
        }
        this.accountService.login(login).subscribe(
          res => {
            this.authorize(res);
          },
          err => {
              console.log(err);
          });
        }
  else
  {
    register.name = this.formModel.value.name; 
    register.password = this.formModel.value.passwords.password;
    register.confirmPassword = this.formModel.value.passwords.confirmPassword;
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
  


