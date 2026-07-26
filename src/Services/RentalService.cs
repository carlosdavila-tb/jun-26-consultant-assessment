using RentalDesk.Models;

namespace RentalDesk.Services;

public class RentalService : IRentalService
{
    private readonly List<Rental> _rentals = new();
    private readonly List<string> _confirmations = new();
    private int _nextId = 1;

    public IReadOnlyList<Rental> GetRentals() => _rentals;

    public IReadOnlyList<string> GetConfirmations() => _confirmations;

    public Rental BookRental(CreateRentalRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerName))
        {
            throw new ArgumentException("Customer name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.VehicleClass))
        {
            throw new ArgumentException("Vehicle class is required.");
        }

        if (request.ReturnDate <= request.PickupDate)
        {
            throw new ArgumentException("Return date must be after the pickup date.");
        }

        if (request.DailyRate <= 0)
        {
            throw new ArgumentException("Daily rate must be greater than zero.");
        }

        int days = request.ReturnDate.DayNumber - request.PickupDate.DayNumber;
        decimal totalCost = days * request.DailyRate;

        var rental = new Rental
        {
            Id = _nextId++,
            CustomerName = request.CustomerName,
            VehicleClass = request.VehicleClass,
            PickupDate = request.PickupDate,
            ReturnDate = request.ReturnDate,
            Days = days,
            DailyRate = request.DailyRate,
            TotalCost = totalCost
        };

        _rentals.Add(rental);

        string summary = $"EMAIL -> {rental.CustomerName}: Your {rental.VehicleClass} is booked for "
            + $"{rental.Days} day(s) from {rental.PickupDate} to {rental.ReturnDate}. Total due: {rental.TotalCost:C}.";
        _confirmations.Add(summary);

        return rental;
    }
    public CreateVehiclePreparation VehiclePreparation(CreateVehiclePreparation request)
    {
        if (string.IsNullOrWhiteSpace(request.CustomerName))
        {
            throw new ArgumentException("Customer name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.VehicleClass))
        {
            throw new ArgumentException("Vehicle class is required.");
        }
        if (request.Rate <= 0)
        {
            throw new ArgumentException("Daily rate must be greater than zero.");
        }
        if (!request.BookingValidation)
        {
            throw new ArgumentException("Booking not validated");
        }
        string PreparationNote = "";
        string ConfirmationMessage = "";
        var vehiclePreparation = new CreateVehiclePreparation
        {
            
        };

        return vehiclePreparation;
    }
}
