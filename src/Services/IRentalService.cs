using RentalDesk.Models.Dto;
using RentalDesk.Models.Entities;

namespace RentalDesk.Services;

public interface IRentalService
{
    IReadOnlyList<Rental> GetRentals();
    IReadOnlyList<string> GetConfirmations();
    Rental BookRental(CreateRentalRequest request);
}
