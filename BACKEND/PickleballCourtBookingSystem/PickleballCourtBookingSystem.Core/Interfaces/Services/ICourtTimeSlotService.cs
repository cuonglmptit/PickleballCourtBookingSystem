using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtTimeSlotService : IBaseService<CourtTimeSlot>
{
    public ServiceResult GetAvailableCourtTimeSlotsByCourtId(Guid? courtId);
    public ServiceResult GetAvailableCourtTimeSlotForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime);

    // public ServiceResult AddCourtTimeSlotWithDefaultPrice(Guid courtId, List<CourtTimeSlot> courtTimeSlots);
}