using PickleballCourtBookingSystem.Core.DTOs;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ITimeService
{
    public DateTime GetCurrentTime();
}