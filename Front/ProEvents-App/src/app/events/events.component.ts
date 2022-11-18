import { Event } from '../models/Event';
import { EventService } from './../services/event.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  public events: Event[] = [];
  public filteredEvents: Event[] = [];

  public widthImg = 50;
  public marginImg = 2;
  public showImg = true;
  private _listFilter = '';

  public get listFilter(): string {
    return this._listFilter;
  }

  public set listFilter(value: string) {
    this._listFilter = value;
    this.filteredEvents = this.listFilter ? this.filterEvent(this.listFilter) : this.events;
  }

  filterEvent(filterBy: string): Event[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter((event: { theme: string; local: string; }) => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  constructor(private eventService: EventService) { }

  public ngOnInit(): void {
    this.getEvents();
  }

  public changeImg(): void {
    this.showImg = !this.showImg;
  }

  public getEvents(): void {
    this.eventService.getEvent().subscribe({
      next: (eventsRes: Event[]) => {
        this.events = eventsRes;
        this.filteredEvents = this.events;
      },
      error: (error: any) => console.log(error)
    });
  }
}
