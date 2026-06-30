export interface Booking {
  id: number;
  customerName: string;
  vehicleClass: string;
  pickupDate: string;
  returnDate: string;
  days: number;
  dailyRate: number;
  totalCost: number;
}
