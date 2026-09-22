namespace RentalDesk.Models.Entities;

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
