import { UserComponent } from './components/user/user.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { ProfileComponent } from './components/user/profile/profile.component';

import { EventsComponent } from './components/events/events.component';
import { EventListComponent } from './components/events/event-list/event-list.component';
import { EventDetailComponent } from './components/events/event-detail/event-detail.component';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { SpeakersComponent } from './components/speakers/speakers.component';
import { ContactsComponent } from './components/contacts/contacts.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },
  { path: 'user/profile', component: ProfileComponent },
  { path: 'events', redirectTo: 'events/list' },
  {
    path: 'events', component: EventsComponent,
    children: [
      { path: 'detail/:id', component: EventDetailComponent },
      { path: 'detail', component: EventDetailComponent },
      { path: 'list', component: EventListComponent }
    ]
  },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'speakers', component: SpeakersComponent },
  { path: 'contacts', component: ContactsComponent },
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: '**', redirectTo: 'dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
