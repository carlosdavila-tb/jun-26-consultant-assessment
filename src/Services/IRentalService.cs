using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IRentalService
{
    IReadOnlyList<Rental> GetRentals();
    IReadOnlyList<string> GetConfirmations();
    IReadOnlyList<DepotNote> GetDepotNotes();

    Rental BookRental(CreateRentalRequest request);
}
