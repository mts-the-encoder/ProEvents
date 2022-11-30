import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {

  form!: FormGroup;

  constructor() { }

  ngOnInit(): void {
    this.validation();
  }

  public validation(): void {
    this.form = new FormGroup({
      theme: new FormControl('',
        [Validators.required, Validators.minLength(4), Validators.maxLength(50), Validators.nullValidator]),
      local: new FormControl('', Validators.required),
      eventDate: new FormControl('',
        [Validators.required, Validators.pattern("MM/dd/yyyy")]),
      qtdPeoples: new FormControl('',
        [Validators.required, Validators.max(120000)]),
      phone: new FormControl('', Validators.required),
      email: new FormControl('',
        [Validators.required, Validators.email]),
      imageURL: new FormControl('', Validators.required)
    });
  }
}
