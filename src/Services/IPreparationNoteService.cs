using RentalDesk.Models.Entities;
using RentalDesk.Models.Dto;

namespace RentalDesk.Services;

public interface IPreparationNoteService
{
    PreparationNote CreatePreparationNote(CreateRentalRequest request, int rentalId);
    IReadOnlyList<PreparationNote> GetPreparationNotes();

}
