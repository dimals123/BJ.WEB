import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/shared/services/account.service';
import { GetAllAccountResponseView } from '../../shared/models/account-views/get-all-account-response-view';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RegisterAccountView } from 'src/app/shared/models/account-views/register-account-view';
import { LoginAccountResponseView } from 'src/app/shared/models/account-views/login-account-response-veiw';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LoginAccountView } from 'src/app/shared/models/account-views/login-account-view';
import { ConfirmPasswordValidator } from 'src/app/shared/validators/confirm-password-validator/confirm-password-validator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {



  constructor(private readonly accountService: AccountService, private readonly router: Router,
    private readonly formBuilder: FormBuilder, private readonly localStorageService: LocalStorageService) { }

  private model = new GetAllAccountResponseView();
  private validPassword: boolean = true;
  private passwordValidator = new ConfirmPasswordValidator;

  public get names(): Array<string> {
    return this.model.names;
  }


  private registerForm = this.formBuilder.group({
    name: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', Validators.required]
  },
    { validator: this.passwordValidator.comparePasswords });


   
  public ngOnInit(): void {
    this.accountService.getAll().subscribe(data => this.model = data);
  }

  private authorize(res: LoginAccountResponseView): void {
    this.localStorageService.setItem('token', res.token);
    this.router.navigate(["game/start-game"]);
  }


  public onSubmit(): void {
    let isExistName = this.names.includes(this.registerForm.value.name);
    var registerModel = new RegisterAccountView();
    var loginModel = new LoginAccountView();
    if (isExistName) {
      loginModel = { ...this.registerForm.value };
      this.accountService.login(loginModel).subscribe(res => {
        this.authorize(res);
      }, err => {
        console.log(err);
      });
    }
    else {
      registerModel = { ...this.registerForm.value }
      this.accountService.register(registerModel).subscribe(res => {
        this.authorize(res);
      }, err => {
        console.log(err);
      });
    }
  }
}



