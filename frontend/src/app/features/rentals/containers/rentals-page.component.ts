import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentalListComponent } from '../components/rental-list.component';
import { TodayRentalListComponent } from '../components/today-rental-list.component';

@Component({
  selector: 'app-rentals-page',
  standalone: true,
  imports: [CommonModule, RentalListComponent, TodayRentalListComponent],
  template: `
    <section class="page">
      <h1>Riverbend Auto Rental — Front Desk</h1>
      <h2>All Rentals</h2>
      <app-rental-list></app-rental-list>
      <br/>
      <app-today-rental-list></app-today-rental-list>
    </section>
  `,
  styles: [`
    .page { max-width: 960px; margin: 0 auto; padding: 24px; font-family: system-ui, sans-serif; }
    h2 { margin-top: 24px; }
  `],
})
export class RentalsDashboardComponent {}
