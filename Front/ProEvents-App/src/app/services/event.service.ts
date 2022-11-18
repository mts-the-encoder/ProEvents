import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  baseURL = 'https://localhost:7242/api/events';
  constructor(private http: HttpClient) { }

  getEvent() {
    return this.http.get(this.baseURL);
  }

}
