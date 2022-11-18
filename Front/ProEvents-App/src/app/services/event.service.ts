import { Event } from '../models/Event';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class EventService {
  baseURL = 'https://localhost:7242/api/events';
  constructor(private http: HttpClient) { }

  public getEvent(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseURL);
  }

  public getEventByTheme(theme: string): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.baseURL}/theme/${theme}`);
  }

  public getEventById(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.baseURL}/${id}`);
  }
}
