import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../../../services/account.service';
import { UserLogin } from './../../../models/identity/UserLogin';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  model = {} as UserLogin;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {}

  public login(): void {
    this.accountService.login(this.model).subscribe(
      () => { this.router.navigateByUrl('/dashboard') },
      (error: any) => {
        if (error.status == 401)
          this.toastr.error('invalid username or password');
        else
          console.error(error);
      }
    )
  }

  fieldTextType!: boolean;

  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }
}
