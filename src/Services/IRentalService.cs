using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IRentalService
{
    IReadOnlyList<Rental> GetRentals();
    IReadOnlyList<string> GetConfirmations();
    IReadOnlyList<string> GetNotes();
    Rental BookRental(CreateRentalRequest request);
}
