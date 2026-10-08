

using RentalDesk.Models;

namespace RentalDesk.Services;

public class DepotPreparationNotes : IBookingListener, IDepotPreparationNotes
{
    private readonly List<string> _notes = new();
    private readonly object _lock = new();

    public IReadOnlyList<string> GetNotes()
    {
        lock (_lock)
        {
            return _notes.ToList();
        }
    }

    public void OnBooked(Rental rental)
    {
        string note = $"DEPOT: {rental.VehicleClass} for pickup on {rental.PickupDate} "
            + $"(booking #{rental.Id}, {rental.CustomerName}, returns on {rental.ReturnDate}).";

        lock (_lock)
        {
            _notes.Add(note);
        }
    }
}
