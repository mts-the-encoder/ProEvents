import { environment } from './../../../../environments/environment.prod';
import { Router } from '@angular/router';
import { Event } from '@app/models/Event';
import { EventService } from '@app/services/event.service';

import { Component, OnInit, TemplateRef } from '@angular/core';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.scss']
})
export class EventListComponent implements OnInit {

  public modalRef!: BsModalRef;
  public events: Event[] = [];
  public filteredEvents: Event[] = [];
  public eventId = 0;

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

  constructor(
    private eventService: EventService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.loadEvents();
  }

  public changeImg(): void {
    this.showImg = !this.showImg;
  }

  public showImage(imageURL: string): string {
    return (imageURL !== '')
      ? `https://localhost:7242/Resources/Images/${imageURL}`
      : 'assets/img/noImage.jpg';
  }

  public loadEvents(): void {
    this.eventService.getEvent().subscribe({
      next: (eventsRes: Event[]) => {
        this.events = eventsRes;
        this.filteredEvents = this.events;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('error to load Events', 'Error!')
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(event: any, template: TemplateRef<any>, eventId: number): void {
    event.stopPropagation();
    this.eventId = eventId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.eventService.delete(this.eventId).subscribe({
      next: (result: any) => {
        console.log(result);
        this.toastr.success('the event was successfully deleted', 'Deleted!');
        this.loadEvents();
      },
      error: (error: any) => {
        this.toastr.error(`failed to delete event ${this.eventId}`, 'Error!');
        console.error(error);
      },
    }).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  eventDetail(id: number): void {
    this.router.navigate([`events/detail/${id}`]);
  }
}

