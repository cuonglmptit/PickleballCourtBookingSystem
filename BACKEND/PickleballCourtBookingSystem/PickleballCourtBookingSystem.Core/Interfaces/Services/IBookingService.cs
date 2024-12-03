using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IBookingService : IBaseService<Booking>
{
    public ServiceResult AddBooking(Guid userId, List<Guid> courtTimeSlotIds, Guid courtId);
    public ServiceResult CancelBooking(Booking booking);
}