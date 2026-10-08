using RentalDesk.Models;

namespace RentalDesk.Services;

public interface IBookingListener
{
    void OnBooked(Rental rental);

}
