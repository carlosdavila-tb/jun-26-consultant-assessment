import { Rental } from '../../models/rental.model';

export function toLocalIsoDate(date: Date): string {
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const day = String(date.getDate()).padStart(2, '0');
  return `${date.getFullYear()}-${month}-${day}`;
}

export function filterTodaysPickups(rentals: Rental[], today: Date = new Date()): Rental[] {
  const todayIso = toLocalIsoDate(today);
  return rentals.filter((rental) => rental.pickupDate.slice(0, 10) === todayIso);
}
