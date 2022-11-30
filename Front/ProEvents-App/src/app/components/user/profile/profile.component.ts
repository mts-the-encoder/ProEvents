import { ValidatorField } from './../../../helpers/ValidatorField';
import { FormGroup, FormBuilder, AbstractControlOptions, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

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
      title: ['', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      function: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
    }, formOptions);
  }

  hide = true;
  get emailInput() { return this.form.get('email'); }
  get passwordInput() { return this.form.get('password'); }

  fieldTextType!: boolean;
  fieldTextTypeConfirmation!: boolean;

  public toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  public toggleFieldTextTypeConfirmation() {
    this.fieldTextTypeConfirmation = !this.fieldTextTypeConfirmation;
  }
}
