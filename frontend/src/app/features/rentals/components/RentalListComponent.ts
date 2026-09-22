import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { RentalService } from '../../../core/services/rental.service';
import { Rental } from '../../../models/rental.model';


@Component({
  selector: 'app-rental-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './RentalListComponent.html',
  styleUrls: ['./RentalListComponent.css']
})
export class RentalListComponent implements OnInit {
  rentals: Rental[] = [];

  constructor(private rentalService: RentalService) { }

  ngOnInit(): void {
    this.rentalService.getRentals().subscribe({
      next: (rentals) => (this.rentals = rentals),
      error: (err) => console.error(err),
    });
  }

  get todayPickups(): Rental[]{
    const today =  new Date().toISOString().split('T')[0];

    return this.rentals.filter(rental => rental.pickupDate === today);
  }
}
