using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IRentalService
{
    IReadOnlyList<Rental> GetRentals();
    IReadOnlyList<string> GetConfirmations();
    IReadOnlyList<string> GetPreparationNotes();
    Rental BookRental(CreateRentalRequest request);
}
