import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Events, TicketSale, EventSales } from '../models';

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient) { }

  fetchEvents(days = 60): Observable<Events[]> {
    return this.http
      .get<Events[]>(`/api/events?days=${days}`)
      .pipe(catchError(this.handleError));
  }

  fetchSalesByEvent(id: string): Observable<TicketSale[]> {
    return this.http
      .get<TicketSale[]>(`/api/tickets/event/${id}`)
      .pipe(catchError(this.handleError));
  }

  fetchTop5Quantity(): Observable<EventSales[]> {
    return this.http
      .get<EventSales[]>(`/api/tickets/top5/quantity`)
      .pipe(catchError(this.handleError));
  }

  fetchTop5Revenue(): Observable<EventSales[]> {
    return this.http
      .get<EventSales[]>(`/api/tickets/top5/revenue`)
      .pipe(catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    console.error('API error', err);
    return throwError(() => new Error(err.message || 'Server error'));
  }
}
