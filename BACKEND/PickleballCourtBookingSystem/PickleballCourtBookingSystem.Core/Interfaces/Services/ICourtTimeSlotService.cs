using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtTimeSlotService : IBaseService<CourtTimeSlot>
{
    public ServiceResult CheckTimeSlotAvailableAndCheckPrice(CourtTimeSlot courtTimeSlot);
}