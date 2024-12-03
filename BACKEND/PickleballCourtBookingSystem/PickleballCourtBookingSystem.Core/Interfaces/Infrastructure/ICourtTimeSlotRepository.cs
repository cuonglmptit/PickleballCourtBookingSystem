using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

public interface ICourtTimeSlotRepository : IBaseRepository<CourtTimeSlot>
{
    public IEnumerable<CourtTimeSlot> FindCourtTimeSlots(Guid? courtId);
}