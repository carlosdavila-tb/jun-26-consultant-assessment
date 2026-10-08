using RentalDesk.Models;
using RentalDesk.Services;

namespace RentalDesk.Tests;

public class RentalServiceTests
{
    private static CreateRentalRequest ValidRequest() => new()
    {
        CustomerName = "Randy Chanchay",
        VehicleClass = "SUV",
        PickupDate = new DateOnly(2026, 6, 1),
        ReturnDate = new DateOnly(2026, 6, 4),
        DailyRate = 42.00m
    };

    private static (RentalService Service, DepotPreparationNotes Depot) CreateService()
    {
        var depot = new DepotPreparationNotes();
        return (new RentalService(new IBookingListener[] { depot }), depot);
    }

    [Fact]
    public void BookRental_StillProducesCustomerConfirmation()
    {
        var (service, _) = CreateService();

        Rental rental = service.BookRental(ValidRequest());

        string confirmation = Assert.Single(service.GetConfirmations());
        Assert.Equal(
            $"EMAIL -> Randy Chanchay: Your SUV is booked for 3 day(s) from {rental.PickupDate} to {rental.ReturnDate}. Total due: {rental.TotalCost:C}.",
            confirmation);
    }

    [Fact]
    public void BookRental_RecordsDepotPreparationNote_ForTheBooking()
    {
        var (service, depot) = CreateService();

        Rental rental = service.BookRental(ValidRequest());

        string note = Assert.Single(depot.GetNotes());
        Assert.Equal(
            $"DEPOT: SUV for pickup on {rental.PickupDate} (booking #1, Randy Chanchay, returns on {rental.ReturnDate}).",
            note);
    }

    [Fact]
    public void BookRental_KeepsDaysTotalAndStoredRecordUnchanged()
    {
        var (service, _) = CreateService();

        Rental rental = service.BookRental(ValidRequest());

        Assert.Equal(1, rental.Id);
        Assert.Equal(3, rental.Days);
        Assert.Equal(126.00m, rental.TotalCost);
        Assert.Equal(42.00m, rental.DailyRate);
        Assert.Equal("Randy Chanchay", rental.CustomerName);
        Assert.Equal("SUV", rental.VehicleClass);
        Assert.Equal(new DateOnly(2026, 6, 1), rental.PickupDate);
        Assert.Equal(new DateOnly(2026, 6, 4), rental.ReturnDate);
        Assert.Same(rental, Assert.Single(service.GetRentals()));
    }

    [Fact]
    public void BookRental_InvalidRequest_StillThrows_AndRecordsNoNote()
    {
        var (service, depot) = CreateService();
        var request = ValidRequest();
        request.CustomerName = "";

        var ex = Assert.Throws<ArgumentException>(() => service.BookRental(request));

        Assert.Equal("Customer name is required.", ex.Message);
        Assert.Empty(service.GetRentals());
        Assert.Empty(service.GetConfirmations());
        Assert.Empty(depot.GetNotes());
    }
}
