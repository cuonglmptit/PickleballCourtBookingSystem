using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IBookingService : IBaseService<Booking>
{
    public ServiceResult AddBooking(User user, List<CourtTimeSlot> courtTimeSlots, Court court);
    public ServiceResult CancelBooking(Booking booking);
}