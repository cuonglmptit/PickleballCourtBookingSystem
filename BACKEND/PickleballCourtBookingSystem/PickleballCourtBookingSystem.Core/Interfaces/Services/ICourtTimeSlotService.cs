using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtTimeSlotService : IBaseService<CourtTimeSlot>
{
    public ServiceResult GetAvailableCourtTimeSlotsByCourtId(Guid? courtId);
    public ServiceResult GetAvailableCourtTimeSlotForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime);

    public ServiceResult AddCourtTimeSlotWithDefaultPrice(List<Guid> courtIds, IEnumerable<DateTime> dates, IEnumerable<CourtPrice> prices);
    public ServiceResult GetAvailableCourtTimeSlotsByCourtIdAndDate(Guid courtId, DateTime date);

    ServiceResult FindCourtTimeSlotsByCourtId(Guid courtId, DateTime date, TimeSpan? time = null);
}