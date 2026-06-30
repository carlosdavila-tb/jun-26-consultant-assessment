import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RentalService } from '../../../core/services/rental.service';
import { Rental } from '../../../models/rental.model';
import { RentalListComponent } from '../components/rental-list.component';

@Component({
  selector: 'app-rentals-page',
  standalone: true,
  imports: [CommonModule, RentalListComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>

      <h2>Todays Pickups</h2>
      <app-rental-list [rentals]="todaysPickups"></app-rental-list>

      <h2>All the Rentals</h2>
      <app-rental-list [rentals]="rentals"></app-rental-list>
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
        this.todaysPickups = rentals.filter(
          (rental) => rental.pickupDate === this.today()
        );
      },
      error: (err) => console.error(err),
    });
  }

  private today(): string {
    return new Date().toISOString().slice(0, 10);
  }
}
