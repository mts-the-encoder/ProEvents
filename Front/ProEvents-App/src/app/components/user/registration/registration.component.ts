import { ValidatorField } from './../../../helpers/ValidatorField';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  form!: FormGroup;

  constructor(public fb: FormBuilder) { }

  get f(): any { return this.form.controls; }

  ngOnInit(): void {
    this.validation();
  }

  private validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmPassword'),
    };

    this.form = this.fb.group({
      firstName: ['', Validators.required] ,
      lastName: ['', Validators.required] ,
      email: ['', [Validators.required, Validators.email]] ,
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]] ,
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    }, formOptions);
  }

  hide = true;
  get passwordInput() { return this.form.get('password'); }

  fieldTextType!: boolean;
  fieldTextTypeConfirmation!: boolean;

  public toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  public toggleFieldTextTypeConfirmation() {
    this.fieldTextTypeConfirmation = !this.fieldTextTypeConfirmation;
  }

  //alert('SUCCESS!! :-)\n\n' + JSON.stringify(this.registerForm.value, null, 4));
}
