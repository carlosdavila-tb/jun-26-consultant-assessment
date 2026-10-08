import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentalListComponent } from '../components/rental-list.component';
import { Rental } from '../../../models/rental.model';
import { RentalService } from '../../../core/services/rental.service';
import { filterTodaysPickups } from '../rental-filters';

@Component({
  selector: 'app-rentals-page',
  standalone: true,
  imports: [CommonModule, RentalListComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>
      <h2>All Rentals</h2>
      <app-rental-list [rentals]="rentals"></app-rental-list>
      <h2>Today's pickups</h2>
      <app-rental-list [rentals]="todaysPickups" emptyMessage="No pickups today."></app-rental-list>
    
    </section>
  `,
  styles: [`
    .page { max-width: 960px; margin: 0 auto; padding: 24px; font-family: system-ui, sans-serif; }
    h2 { margin-top: 24px; }
  `],
})
export class RentalsDashboardComponent implements OnInit {
  rentals: Rental[] = [];
  todaysPickups: Rental[] = [];

  constructor(private rentalService: RentalService) {}

  ngOnInit(): void {
    this.rentalService.getRentals().subscribe({
      next: (rentals) => {
        this.rentals = rentals;
        this.todaysPickups = filterTodaysPickups(rentals);
      },
      error: (err) => console.error(err),
    });
  }
}
