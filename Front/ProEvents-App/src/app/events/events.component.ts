import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  public events: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {

    this.http.get('https://localhost:7242/api/events').subscribe(
      response => this.events = response,
      error => console.log(error)
    );

    this.getEvents();
  }

  public getEvents(): any {

  }
}
