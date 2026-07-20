using Microsoft.AspNetCore.Mvc;
using RentalDesk.Models;
using RentalDesk.Services;

namespace RentalDesk.Controllers;

[ApiController]
[Route("rentals")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
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

    [HttpGet("preparations")]
    public ActionResult<IReadOnlyList<string>> GetPreparationNotes()
    {
        return Ok(_rentalService.GetPreparationNotes());
    }

    [HttpPost]
    public ActionResult<Rental> BookRental([FromBody] CreateRentalRequest request)
    {
        try
        {
            Rental rental = _rentalService.BookRental(request);
            return CreatedAtAction(nameof(GetRentals), new { id = rental.Id }, rental);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}