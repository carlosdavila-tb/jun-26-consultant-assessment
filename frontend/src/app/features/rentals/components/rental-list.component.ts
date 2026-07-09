import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentalService } from '../../../core/services/rental.service';
import { Rental } from '../../../models/rental.model';

@Component({
  selector: 'app-rental-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <table class="rental-table">
      <thead>
        <tr>
          <th>Customer</th>
          <th>Vehicle</th>
          <th>Pickup</th>
          <th>Return</th>
          <th>Days</th>
          <th>Total</th>
        </tr>
      </thead>
      <tbody>
        @for (rental of rentals; track rental.id) {
          <tr>
            <td>{{ rental.customerName }}</td>
            <td>{{ rental.vehicleClass }}</td>
            <td>{{ rental.pickupDate }}</td>
            <td>{{ rental.returnDate }}</td>
            <td>{{ rental.days }}</td>
            <td>{{ rental.totalCost | currency }}</td>
          </tr>
        }
        @empty {
          <tr>
            <td>No rentals to show.</td>
          </tr>
        }
      </tbody>
    </table>
    <br>
    <br>
    <h2>Today's pickup</h2>
    <table class="rental-table">
      <thead>
        <tr>
          <th>Customer</th>
          <th>Vehicle</th>
          <th>Pickup</th>
          <th>Return</th>
          <th>Days</th>
          <th>Total</th>
        </tr>
      </thead>
      <tbody>
        @for (rental of pickUpsoday; track rental.id) {
          <tr>
            <td>{{ rental.customerName }}</td>
            <td>{{ rental.vehicleClass }}</td>
            <td>{{ rental.pickupDate }}</td>
            <td>{{ rental.returnDate }}</td>
            <td>{{ rental.days }}</td>
            <td>{{ rental.totalCost | currency }}</td>
          </tr>
        }
        @empty {
          <tr>
            <td>No rentals to show.</td>
          </tr>
        }
      </tbody>
    </table>
   
  `,
  styles: [`
    .rental-table { width: 100%; border-collapse: collapse; }
    .rental-table th, .rental-table td { border-bottom: 1px solid #ddd; padding: 8px; text-align: left; }
  `],
})
export class RentalListComponent implements OnInit {
  rentals: Rental[] = [];
  pickUpsoday: Rental[] = [];

  //I know there is a way to make a templtate to avoid copying the table but i didn't remember it
  constructor(private rentalService: RentalService) {}

  ngOnInit(): void {
    this.rentalService.getRentals().subscribe({
      next: (rentals) => {
        const today = new Date;
        const year = today.getFullYear();
        const month = today.getMonth() + 1;
        const day = today.getDate();

        const todayFormatted = `${year}-${String(month).padStart(2, "0")}-${String(day).padStart(2, "0")}`;
        console.log(todayFormatted);

        this.rentals = rentals;
        this.pickUpsoday = rentals.filter(rental => rental.pickupDate === todayFormatted)
      },
      error: (err) => console.error(err),
    });
  }
}
