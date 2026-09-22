using RentalDesk.Models;

namespace RentalDesk.Services;

public class DepotNoteService : IDepotNoteService
{
    private readonly List<DepotNote> _notes = new();

    public void Record(Rental rental)
    {
        var note = new DepotNote
        {
            RentalId    = rental.Id,
            CustomerName = rental.CustomerName,
            VehicleClass = rental.VehicleClass,
            PickupDate   = rental.PickupDate,
            PreparedAt   = DateTime.UtcNow,
            Note         = $"DEPOT → Prepare {rental.VehicleClass} for {rental.CustomerName}, pickup {rental.PickupDate}."
        };

        _notes.Add(note);
    }

    public IReadOnlyList<DepotNote> GetNotes() => _notes;
}