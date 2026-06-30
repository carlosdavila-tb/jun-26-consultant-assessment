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

    [HttpGet]
    public ActionResult<IReadOnlyList<Rental>> GetRentalsToday()
    {
        return Ok(_rentalService.GetRentalsToday());
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

            VehiclePreparationNote vehiclePreparationNote = new()
            {
                Id = 0, // Si la BD genera el Id, puedes dejar 0.
                VehicleClass = rental.VehicleClass,
                PreparationDate = DateOnly.FromDateTime(DateTime.Today),
                ReadyForRent = false,
                PreparationNote = string.Empty
            };

            // Aquí podrías guardarlo
            // _vehiclePreparationNoteService.Create(vehiclePreparationNote);

            return CreatedAtAction(nameof(GetRentals), new { id = rental.Id }, rental);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
