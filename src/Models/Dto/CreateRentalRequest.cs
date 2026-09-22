namespace RentalDesk.Models.Dto;

public class CreateRentalRequest
{
    public string CustomerName { get; set; } = string.Empty;
    public string VehicleClass { get; set; } = string.Empty;
    public DateOnly PickupDate { get; set; }
    public DateOnly ReturnDate { get; set; }
    public decimal DailyRate { get; set; }
    public string Note {get;set;} = string.Empty;
}
