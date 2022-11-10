import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements OnInit {

  public events: any = [];
  public filteredEvents: any = [];
  widthImg: number = 50;
  marginImg: number = 2;
  showImg: boolean = true;
  private _listFilter : string = '';

  public get listFilter() {
    return this._listFilter;
  }

  public set listFilter(value: string) {
    this._listFilter = value;
    this.filteredEvents = this.listFilter ? this.filterEvent(this.listFilter) : this.events;
  }

  filterEvent(filterBy: string): any {
    filterBy = filterBy.toLocaleLowerCase();
    return this.events.filter((event: { theme: string; place: string; }) => event.theme.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
    event.place.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEvents();
  }

  changeImg() {
    this.showImg = !this.showImg;
  }

  public getEvents(): void {
    this.http.get('https://localhost:7242/api/events').subscribe(
      response => {
        this.events = response;
        this.filteredEvents = this.events
      },
      error => console.log(error)
      );
  }
}
