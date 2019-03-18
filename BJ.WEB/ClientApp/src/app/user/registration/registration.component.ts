import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { User } from '../user';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {


  users = Array<User>();
  constructor(private service:UserService) { }


  ngOnInit() {
    this.users = this.service.users;
  }

  onSubmit() {
    this.service.register().subscribe(
      (res: any) => {
        if (res.succeeded) {
          this.service.formModel.reset();
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

