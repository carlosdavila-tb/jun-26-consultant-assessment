using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IDepotNoteService
{
    void Record(Rental rental);
    IReadOnlyList<DepotNote> GetNotes();
}