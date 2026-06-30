namespace RentalDesk.Models;

public class VehiclePreparationNote
{
    public required int Id { get; init; }
    public string VehicleId { get; init; }
    public required string VehicleClass { get; init; }
    public required DateOnly PreparationDate { get; init; }
    public required bool ReadyForRent { get; init; }
    public bool PreparationMade { get; init; }
    public required string PreparationNote { get; init; }
}

public class CreateVehiclePreparationNote
{
    public string VehicleId { get; set; } = string.Empty;
    public string VehicleClass { get; set; } = string.Empty;
    public DateOnly PreparationDate { get; set; }
    public bool ReadyForRent { get; set; }
    public string PreparationNote { get; set; }
}
