using System;

namespace RentalDesk.Services;

public interface IDepotPreparationNotes
{
    IReadOnlyList<string> GetNotes();
}
