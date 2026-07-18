import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BookingListComponent } from '../components/booking-list.component';

@Component({
  selector: 'app-bookings-page',
  standalone: true,
  imports: [CommonModule, BookingListComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>
      <h2>All Bookings</h2>
      <app-booking-list></app-booking-list>
    </section>
  `,
  styles: [`
    .page { max-width: 960px; margin: 0 auto; padding: 24px; font-family: system-ui, sans-serif; }
    h2 { margin-top: 24px; }
  `],
})
export class BookingsDashboardComponent {}
