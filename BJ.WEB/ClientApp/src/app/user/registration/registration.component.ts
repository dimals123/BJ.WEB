import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { GetAllAccountResponseView } from '../user';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {


  
  constructor(private service:UserService, private router: Router) { }
  users = new GetAllAccountResponseView();

  ngOnInit() {
    if (localStorage.getItem('token') != null)
      this.router.navigateByUrl('/game');
   this.service.getNames().subscribe(data=>this.users = data["accountNames"]);
   
  }
  onSubmit(form:NgForm) {

    debugger;
    if(this.users.name.find(form.value.UserName) != undefined)
  {
        this.service.login(form.value).subscribe(
          (res: any) => {
            localStorage.setItem('token', res.token);
            this.router.navigateByUrl('/game');
          },
          err => {
              console.log(err);

    });
    
  } 
  else
  {
    this.service.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
        } else {
          res.errors.forEach(element => {
            switch (element.code) {
              case 'DuplicateUserName':
                
                break;

              default:
              
                break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
    } 
  }
  

}

