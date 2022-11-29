import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';

import { EventListComponent } from '@app/components/events/event-list/event-list.component';
import { EventDetailComponent } from '@app/components/events/event-detail/event-detail.component';
import { AppRoutingModule } from '@app/app-routing.module';
import { AppComponent } from '@app/app.component';
import { SpeakersComponent } from '@app/components/speakers/speakers.component';
import { EventsComponent } from '@app/components/events/events.component';
import { NavComponent } from '@app/shared/nav/nav.component';
import { EventService } from '@app/services/event.service';

import { DateTimeFormatPipe } from '@app/helpers/DateTimeFormat.pipe';
import { TitleComponent } from '@app/shared/title/title.component';
import { DashboardComponent } from '@app/components/dashboard/dashboard.component';
import { ContactsComponent } from '@app/components/contacts/contacts.component';
import { ProfileComponent } from '@app/components/user/profile/profile.component';
import { UserComponent } from '@app/components/user/user.component';
import { LoginComponent } from '@app/components/user/login/login.component';
import { RegistrationComponent } from '@app/components/user/registration/registration.component';

@NgModule({
  declarations: [
    AppComponent,
    SpeakersComponent,
    EventsComponent,
    NavComponent,
    DateTimeFormatPipe,
    TitleComponent,
    DashboardComponent,
    ContactsComponent,
    ProfileComponent,
    EventDetailComponent,
    EventListComponent,
    UserComponent,
    LoginComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    FormsModule,
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true,
      progressAnimation: 'decreasing'
    }),
    NgxSpinnerModule
  ],
  providers: [
    EventService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
