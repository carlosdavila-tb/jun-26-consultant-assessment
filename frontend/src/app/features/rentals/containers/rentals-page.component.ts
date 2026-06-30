import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentalListComponent } from '../components/rental-list.component';
import { TodayListComponent } from '../components/today-list.component';

@Component({
  selector: 'app-rentals-page',
  standalone: true,
  imports: [CommonModule, RentalListComponent, TodayListComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>
      <h2>All Rentals</h2>
      <app-rental-list></app-rental-list>
      <h2>Today's pickups</h2>
      <app-today-list></app-today-list>
    </section>
  `,
  styles: [`
    .page { max-width: 960px; margin: 0 auto; padding: 24px; font-family: system-ui, sans-serif; }
    h2 { margin-top: 24px; }
  `],
})
export class RentalsDashboardComponent {}
