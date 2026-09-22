using RentalDesk.Models.Entities;
using RentalDesk.Models.Dto;

namespace RentalDesk.Services;

public class PreparationNoteService : IPreparationNoteService
{
    private readonly List<PreparationNote> _preparationNotes = new();
    private int _nextId = 1;

    public IReadOnlyList<PreparationNote> GetPreparationNotes() => _preparationNotes;

    public PreparationNote CreatePreparationNote(CreateRentalRequest request, int rentalId)
    {
        
        if (string.IsNullOrWhiteSpace(request.Note))
        {
            throw new ArgumentException("Note is required.");
        }

        var preparationNote = new PreparationNote
        {
            Id = _nextId++,
            Note = request.Note,
            RentalId = rentalId
        };


        _preparationNotes.Add(preparationNote);

        return preparationNote;
    }
}
