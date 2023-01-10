import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../../services/account.service';
import { Component, OnInit } from '@angular/core';
import { UserLogin } from '@app/models/identity/UserLogin';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {}
}
