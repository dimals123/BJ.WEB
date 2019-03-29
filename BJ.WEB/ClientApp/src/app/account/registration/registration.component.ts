import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/shared/services/account.service';
import { GetAllAccountResponseView } from '../../shared/models/account-views/get-all-account-response-view';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';
import { RegisterAccountView } from 'src/app/shared/models/account-views/register-account-view';
import { LoginAccountResponseView } from 'src/app/shared/models/account-views/login-account-response-veiw';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit 
{


  
  constructor(private service:AccountService, private router: Router, private formbuilder:FormBuilder) { }
  users = new GetAllAccountResponseView();

  formModel = this.formbuilder.group({
    Name:['', Validators.required],
    Password:['', Validators.required]
  });


  ngOnInit(): void {
   this.service.getNames().subscribe(data=>this.users.names = data["accountNames"]);
  }

  authorize(res:LoginAccountResponseView): void{
    localStorage.setItem('token', res.token);
    localStorage.setItem('userId', res.userId)
    this.router.navigateByUrl('/game');
  }


  onSubmit() :void
  {
    var register = new RegisterAccountView();
    register = this.formModel.value; 
      if(this.users.names.indexOf(this.formModel.value.Name) != -1)
      {
        this.service.login(register).subscribe(
          res => {
            this.authorize(res);
          },
          err => {
              console.log(err);
          });
        }
  else
  {
    this.service.register(register).subscribe(
      res => {
        this.authorize(res);
      },
      err => {
        console.log(err);
      });
  } 
  }
}
  


