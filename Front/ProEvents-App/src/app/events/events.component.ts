import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  public events: any = [
    {
      Theme: 'SQL Server',
      City: 'SP'
    },
    {
      Theme: '.NET',
      City: 'SP'
    },
    {
      Theme: 'Angular',
      City: 'SP'
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }
}
