import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {

  form!: FormGroup;

  constructor() { }

  ngOnInit(): void {
  }

  public validation(): void {
    this.form = new FormGroup({
      local: new FormControl(),
      eventDate: new FormControl(),
      theme: new FormControl(),
      qtdPeoples: new FormControl(),
      imageURL: new FormControl(),
      phone: new FormControl(),
      email: new FormControl()
    });
  }

}
