import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/shared/services/account.service';
import { GetAllAccountResponseView } from '../../shared/models/account-views/get-all-account-response-view';
import { Router } from '@angular/router';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RegisterAccountView } from 'src/app/shared/models/account-views/register-account-view';
import { LoginAccountResponseView } from 'src/app/shared/models/account-views/login-account-response-veiw';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { LoginAccountView } from 'src/app/shared/models/account-views/login-account-view';
import { MenuComponent } from 'src/app/shared/loyaot/menu/menu.component';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {



  constructor(private readonly accountService: AccountService, private readonly router: Router,
    private readonly formBuilder: FormBuilder, private readonly localStorageService: LocalStorageService) { }

  private _model = new GetAllAccountResponseView();
  private validPassword: boolean = true;

  public get modelNames(): Array<string> {
    return this._model.names;
  }


  private registerForm = this.formBuilder.group({
    name: ['', Validators.required],
    password: ['', [Validators.required, Validators.minLength(8)]],
    confirmPassword: ['', Validators.required]
  },
    { validator: this.comparePasswords });


  private comparePasswords(formBuilder: FormGroup) {
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
    this.accountService.getAll().subscribe(data => this._model = data);
  }

  private authorize(res: LoginAccountResponseView): void {
    this.localStorageService.setItem('token', res.token);
    this.router.navigate(["game/start-game"]);
    window.location.reload();
  }


  public onSubmit(): void {
    if (this.registerForm.value.password == '') {
      this.validPassword = false;
    }
    let SearchName = this.modelNames.includes(this.registerForm.value.name);
    var register = new RegisterAccountView();
    var login = new LoginAccountView();
    if (SearchName) {
      login = { ...this.registerForm.value };
      this.accountService.login(login).subscribe(res => {
        this.authorize(res);
      }, err => {
        console.log(err);
      });
    }
    else {
      register = { ...this.registerForm.value }
      this.accountService.register(register).subscribe(res => {
        this.authorize(res);
      }, err => {
        console.log(err);
      });
    }
  }
}



