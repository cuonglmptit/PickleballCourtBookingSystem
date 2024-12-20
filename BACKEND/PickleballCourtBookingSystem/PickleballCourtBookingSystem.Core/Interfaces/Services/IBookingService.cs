using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IBookingService : IBaseService<Booking>
{
    public ServiceResult AddBooking(Guid userId, List<Guid> courtTimeSlotIds, Guid courtId);
    public ServiceResult CourtOwnerConfirmBooking(Guid userId, Guid bookingId);
    public ServiceResult CustomerConfirmBooking(Guid userId, Guid bookingId);
    public ServiceResult CancelBooking(Guid userId, Guid bookingId, string? reason);
}