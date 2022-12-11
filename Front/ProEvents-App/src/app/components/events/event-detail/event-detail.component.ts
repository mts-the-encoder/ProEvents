import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl } from '@angular/forms';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {

  form!: FormGroup;

  get f(): any { return this.form.controls; }

  get bsConfig(): any {
    return { adaptivePosition: true, isAnimated: true, containerClass: 'theme-default', showTodayButton: true, todayPosition: 'center',
    dateInputFormat: 'MM/DD/YYYY hh:mm a', showWeekNumbers: false }
  }

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50), Validators.nullValidator]],
      local: ['', Validators.required],
      eventDate: ['', [Validators.required]],
      qtdPeople: ['', [Validators.required, Validators.max(120000), Validators.min(100)]],
      phone: ['', [Validators.required, Validators.pattern('(([+][(]?[0-9]{1,3}[)]?)|([(]?[0-9]{4}[)]?))\s*[)]?[-\s\.]?[(]?[0-9]{1,3}[)]?([-\s\.]?[0-9]{3})([-\s\.]?[0-9]{3,4})')]],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required]
      //validator: MustMatch('password', 'confirmPassword')
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(formField: FormControl): any {
    return {'is-invalid': formField.errors && formField.touched}
  }
}
