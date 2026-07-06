using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IRentalService
{
    IReadOnlyList<Rental> GetRentals();
    IReadOnlyList<string> GetConfirmations();
    Rental BookRental(CreateRentalRequest request);
    CreateVehiclePreparation VehiclePreparation(CreateVehiclePreparation request);
    
}
