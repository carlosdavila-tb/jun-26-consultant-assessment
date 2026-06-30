import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentalListComponent } from '../components/rental-list.component';
import { RentalListTodayComponent } from '../components/rental-list-today.component';


@Component({
  selector: 'app-rentals-page',
  standalone: true,
  imports: [CommonModule, RentalListComponent, RentalListTodayComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>
      <h2>All Rentals</h2>
      <app-rental-list></app-rental-list>
      <h2>Today's pickups</h2>
      <app-rental-list-today></app-rental-list-today>
    </section>
  `,
  styles: [`
    .page { max-width: 960px; margin: 0 auto; padding: 24px; font-family: system-ui, sans-serif; }
    h2 { margin-top: 24px; }
  `],
})
export class RentalsDashboardComponent {}
