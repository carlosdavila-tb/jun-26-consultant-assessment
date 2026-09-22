using Microsoft.AspNetCore.Mvc;
using RentalDesk.Models.Dto;
using RentalDesk.Models.Entities;
using RentalDesk.Services;

namespace RentalDesk.Controllers;

[ApiController]
[Route("rentals")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;
    private readonly IPreparationNoteService _preparationNoteService;

    public RentalsController(IRentalService rentalService, IPreparationNoteService preparationNoteSerivce)
    {
        _rentalService = rentalService;
        _preparationNoteService = preparationNoteSerivce;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<Rental>> GetRentals()
    {
        return Ok(_rentalService.GetRentals());
    }

    [HttpGet("confirmations")]
    public ActionResult<IReadOnlyList<string>> GetConfirmations()
    {
        return Ok(_rentalService.GetConfirmations());
    }

    [HttpPost]
    public ActionResult<Rental> BookRental([FromBody] CreateRentalRequest request)
    {
        try
        {
            Rental rental = _rentalService.BookRental(request);
            PreparationNote preparationNote = _preparationNoteService.CreatePreparationNote(request, rental.Id);

            return CreatedAtAction(nameof(GetRentals), new { id = rental.Id }, rental);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

     [HttpGet("preparation-notes")]
    public ActionResult<IReadOnlyList<PreparationNote>> GetNotes()
    {
        return Ok(_preparationNoteService.GetPreparationNotes());
    }
}
