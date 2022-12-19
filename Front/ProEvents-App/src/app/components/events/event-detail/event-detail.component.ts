import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Event } from './../../../models/Event';
import { EventService } from './../../../services/event.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  FormGroup,
  Validators,
  FormBuilder,
  FormControl,
  FormArray,
  AbstractControl,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Lot } from '@app/models/Lot';
import { LotService } from '@app/services/lot.service';

@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.scss'],
})
export class EventDetailComponent implements OnInit {

  modalRef!: BsModalRef;
  eventId!: number;
  event = {} as Event;
  form!: FormGroup;
  saveMode = 'post';
  currentLot = { id: 0, name: '', index: 0 };
  imageURL = 'assets/img/upload.png';

  get editMode(): boolean {
    return this.saveMode === 'put';
  }

  get lots(): FormArray {
    return this.form.get('lots') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      isAnimated: true,
      containerClass: 'theme-default',
      showTodayButton: true,
      todayPosition: 'center',
      dateInputFormat: 'MM/DD/YYYY hh:mm a',
      showWeekNumbers: false,
    };
  }

  get bsConfigLot(): any {
    return {
      adaptivePosition: true,
      isAnimated: true,
      containerClass: 'theme-default',
      showTodayButton: true,
      todayPosition: 'center',
      dateInputFormat: 'MM/DD/YYYY',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private activatedRouter: ActivatedRoute,
    private eventService: EventService,
    private modalService: BsModalService,
    private lotService: LotService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadEvent();
    this.validation();
  }

  public loadEvent(): void {
    this.eventId = Number(this.activatedRouter.snapshot.paramMap.get('id'));

    if (this.eventId !== null && this.eventId !== 0) {
      this.spinner.show();
      this.saveMode = 'put';
      this.eventService.getEventById(this.eventId).subscribe({
        next: (event: Event) => {
          this.event = { ...event };
          this.form.patchValue(this.event);
          this.loadLots();
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toastr.error('cannot load event', 'Error!');
          console.log(error);
        },
        complete: () => this.spinner.hide(),
      });
    }
  }

  public validation(): void {
    this.form = this.fb.group({
      theme: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
          Validators.nullValidator,
        ],
      ],
      local: ['', Validators.required],
      eventDate: ['', [Validators.required]],
      qtdPeople: [
        '',
        [Validators.required, Validators.max(120000), Validators.min(100)],
      ],
      phone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required],
      lots: this.fb.array([]),
    });
  }

  addLot(): void {
    this.lots.push(this.createLot({ id: 0 } as Lot));
  }

  createLot(lot: Lot): FormGroup {
    return this.fb.group({
      id: [lot.id],
      name: [lot.name, Validators.required],
      qtd: [lot.qtd, Validators.required],
      price: [lot.price, Validators.required],
      startDate: [lot.startDate],
      endDate: [lot.endDate],
    });
  }

  public changeValueData(value: Date, i: number, field: string): void {
    this.lots.value[i][field] = value;
  }

  public returnTitle(name: string): string {
    return name === null || name === '' ? 'Lot Name' : name;
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(formField: FormControl | AbstractControl | null): any {
    return { 'is-invalid': formField?.errors && formField?.touched };
  }

  public saveEvent(): void {
    this.spinner.show();
    if (this.form.valid) {
      if (this.saveMode === 'post') {
        this.event = { ...this.form.value };
        this.eventService.post(this.event).subscribe({
          next: (eventReturn: Event) => {
            this.toastr.success('event has been saved successfully', 'Success');
            this.router.navigate([`events/detail/${eventReturn.id}`]);
          },
          error: (error: any) => {
            console.error(error);
            this.spinner.hide();
            this.toastr.error('error to save event', 'Error');
          },
          complete: () => this.spinner.hide(),
        });
      } else {
        this.event = { id: this.event.id, ...this.form.value };
        this.eventService.put(this.event).subscribe({
          next: (eventReturn: Event) => {
            this.toastr.success('event has been saved successfully', 'Success');
            this.router.navigate([`events/detail/${eventReturn.id}`]);
          },
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

  public saveLots(): void {
    if (this.form.controls['lots'].valid) {
      this.spinner.show();
      this.lotService
        .saveLot(this.eventId, this.form.value.lots)
        .subscribe({
          next: () => {
            this.toastr.success('Lots successfully saved', 'Success');
            this.lots.reset();
          },
          error: (error: any) => {
            this.toastr.error('error to save lots', 'Error');
            console.error(error);
          },
        })
        .add(() => this.spinner.hide());
    }
  }

  public loadLots(): void {
    this.lotService.getLotsByEventId(this.eventId).subscribe({
      next: (lotsReturn: Lot[]) => {
        lotsReturn.forEach(lot => {
          this.lots.push(this.createLot(lot));
        });
      },
      error: (error: any) => {
        this.toastr.error('error to load lots', 'Error');
        console.error(error);
      },
    })
      .add(() => this.spinner.hide());
  }

  public removeLot(template: TemplateRef<any>, index: number): void {
    this.currentLot.id = this.lots.get(index + '.id')!.value;
    this.currentLot.name = this.lots.get(index + '.name')!.value;
    this.currentLot.index = index;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public confirmDeleteLot(): void {
    this.modalRef.hide();
    this.spinner.show();
    this.lotService.delete(this.eventId, this.currentLot.id)
      .subscribe({
        next: () => {
          this.toastr.success('lot deleted successfully', 'Success');
          this.lots.removeAt(this.currentLot.index);
        },
        error: (error: any) => {
          this.toastr.error(`error to delete lot ${this.currentLot.id}`, 'Error');
          console.error(error);
        },
      }).add(() => this.spinner.hide());
  }

  public declineDeleteLot(): void {
    this.modalRef.hide();
  }
}

