using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IRentalService
{
    IReadOnlyList<Rental> GetRentals();
    IReadOnlyList<Rental> GetRentalsToday();
    IReadOnlyList<string> GetConfirmations();
    Rental BookRental(CreateRentalRequest request);
}