import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Event } from './../../../models/Event';
import { EventService } from './../../../services/event.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder, FormControl, FormArray } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Lot } from '@app/models/Lot';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss']
})
export class EventDetailComponent implements OnInit {

  event = {} as Event;
  form!: FormGroup;
  saveMode = 'post';

  get f(): any { return this.form.controls; }

  get lots(): FormArray { return this.form.get('lots') as FormArray }

  get bsConfig(): any {
    return {
      adaptivePosition: true, isAnimated: true, containerClass: 'theme-default', showTodayButton: true, todayPosition: 'center',
      dateInputFormat: 'MM/DD/YYYY hh:mm a', showWeekNumbers: false
    }
  }

  constructor(
    private fb: FormBuilder,
    private router: ActivatedRoute,
    private eventService: EventService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.loadEvent();
    this.validation();
  }

  public loadEvent(): void {
    const eventIdParam = this.router.snapshot.paramMap.get('id');

    if (eventIdParam !== null) {
      this.spinner.show();

      this.saveMode = 'put';

      this.eventService.getEventById(+eventIdParam).subscribe({
        next: (event: Event) => {
          this.event = { ...event };
          this.form.patchValue(this.event);
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('cannot load event', 'Error!')
          console.log(error);
        },
        complete: () => this.spinner.hide(),
      });
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50), Validators.nullValidator]],
      local: ['', Validators.required],
      eventDate: ['', [Validators.required]],
      qtdPeople: ['', [Validators.required, Validators.max(120000), Validators.min(100)]],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required],
      lotes: this.fb.array([])
    });
  }

  createLot(lot: Lot): FormGroup{
    return this.fb.group({
      id: [lot.id],
        name: [lot.name, Validators.required],
        price: [lot.price, Validators.required],
        startDate: [lot.startDate],
        endDate: [lot.endDate]
    });
  }

  addLot(): void {
    this.lots.push(this.createLot({id: 0} as Lot));
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(formField: FormControl): any {
    return { 'is-invalid': formField.errors && formField.touched }
  }

  public saveChanges(): void {
    this.spinner.show();
    if (this.form.valid) {
      if (this.saveMode === 'post') {
        this.event = { ...this.form.value };
        this.eventService.post(this.event).subscribe({
          next: () => this.toastr.success('event has been saved successfully', 'Success'),
          error: (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('error to save event', 'Error');
          },
          complete: () => this.spinner.hide(),
        });
      } else {
        this.event = {id: this.event.id, ...this.form.value};
          this.eventService.put(this.event).subscribe({
          next: () => this.toastr.success('event has been saved successfully', 'Success'),
          error: (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('error to save event', 'Error');
          },
          complete: () => this.spinner.hide(),
        });
      }
    }
  }
}
