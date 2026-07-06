namespace RentalDesk.Models;

public class Rental
{
    public required int Id { get; init; }
    public required string CustomerName { get; init; }
    public required string VehicleClass { get; init; }
    public required DateOnly PickupDate { get; init; }
    public required DateOnly ReturnDate { get; init; }
    public required int Days { get; init; }
    public required decimal DailyRate { get; init; }
    public required decimal TotalCost { get; init; }
}

public class CreateRentalRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public string VehicleClass { get; set; } = string.Empty;
    public DateOnly PickupDate { get; set; }
    public DateOnly ReturnDate { get; set; }
    public decimal DailyRate { get; set; }
}

public class CreateVehiclePreparation
{
    public string CustomerName {get; set;}
    public string VehicleClass {get; set;}
    public DateOnly PickupDate {get; set;}
    public string PreparationNote {get; set;}
    public string ConfirmationMessage {get; set;}
    public Boolean BookingValidation {get; set;}
    public decimal Rate {get; set;}
}