import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Lot } from '@app/models/Lot';

@Injectable()

export class LotService {

  baseURL = 'https://localhost:7242/api/lots';
  constructor(private http: HttpClient) { }

  public getLotsByEventId(eventId: number): Observable<Lot[]> {
    return this.http
      .get<Lot[]>(`${this.baseURL}/${eventId}`)
      .pipe(take(1));
  }

  public saveLot(eventId: number, lots: Lot[]): Observable<Lot[]> {
    return this.http
      .put<Lot[]>(`${this.baseURL}/${eventId}`, lots)
      .pipe(take(1));
  }

  public delete(eventId: number, lotId: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${eventId}/${lotId}`)
      .pipe(take(1));
  }

}
