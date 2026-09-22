namespace RentalDesk.Models.Entities;

public class PreparationNote
{
    public required int Id { get; init; }
    public required int RentalId { get; init; }
    public required string Note { get; init; }
}
