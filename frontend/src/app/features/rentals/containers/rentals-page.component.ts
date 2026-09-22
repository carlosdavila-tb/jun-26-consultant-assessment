import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentalListComponent } from '../components/rental-list.component';
import { RentalService } from '../../../core/services/rental.service';
import { Rental } from '../../../models/rental.model';


@Component({
  selector: 'app-rentals-page',
  standalone: true,
  imports: [CommonModule, RentalListComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>
      <h2>All Rentals</h2>
      <app-rental-list [rentals]="allRentals"></app-rental-list>
      <h2>Today's Pickups</h2>
      <app-rental-list [rentals]="todaysRentals"></app-rental-list>

    </section>
  `,
  styles: [`
    .page { max-width: 960px; margin: 0 auto; padding: 24px; font-family: system-ui, sans-serif; }
    h2 { margin-top: 24px; }
  `],
})
export class RentalsDashboardComponent implements OnInit {
  allRentals: Rental[] = [];
  todaysRentals: Rental[] = [];
  constructor(private rentalService: RentalService) {}
  ngOnInit(): void {
    this.rentalService.getRentals().subscribe({
      next: (rentals) => {
        this.allRentals = rentals;
        const today = new Date().toISOString().slice(0, 10);
        this.todaysRentals = rentals.filter(r => r.pickupDate === today);
      },
      error: (err) => console.error(err),
    });
  }
}

