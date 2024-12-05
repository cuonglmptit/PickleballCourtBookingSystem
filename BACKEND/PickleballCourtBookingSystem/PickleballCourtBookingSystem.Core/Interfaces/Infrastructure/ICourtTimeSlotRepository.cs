using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

public interface ICourtTimeSlotRepository : IBaseRepository<CourtTimeSlot>
{
    public IEnumerable<CourtTimeSlot> FindAvailableCourtTimeSlotsByCourtId(Guid? courtId, DateTime date, TimeSpan time);
    public IEnumerable<CourtTimeSlot> FindAvailableCourtTimeSlotsForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime);

    public IEnumerable<CourtTimeSlot> FindCourtTimeSlotsByCourtId(Guid? courtId, DateTime date, TimeSpan time);
}