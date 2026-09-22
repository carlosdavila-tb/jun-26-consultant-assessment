namespace RentalDesk.Models;

public class DepotNote
{
    public required int RentalId { get; init; }
    public required string CustomerName { get; init; }
    public required string VehicleClass { get; init; }
    public required DateOnly PickupDate { get; init; }
    public required DateTime PreparedAt { get; init; }
    public required string Note { get; init; }
}