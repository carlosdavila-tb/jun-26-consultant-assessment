import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Rental } from '../../models/rental.model';

@Injectable({ providedIn: 'root' })
export class RentalService {
  private readonly baseUrl = `${environment.apiUrl}/rentals`;

  constructor(private http: HttpClient) {}

  getRentals(): Observable<Rental[]> {
    return this.http.get<Rental[]>(this.baseUrl);
  }

  getRentalsForToday(): Observable<Rental[]> {
    return this.http.get<Rental[]>(this.baseUrl);
  }
}