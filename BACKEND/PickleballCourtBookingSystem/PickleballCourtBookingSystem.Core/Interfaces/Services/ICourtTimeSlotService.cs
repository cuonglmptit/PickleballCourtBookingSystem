using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtTimeSlotService : IBaseService<CourtTimeSlot>
{
    public ServiceResult GetCourtTimeSlot(Guid? courtId);
}