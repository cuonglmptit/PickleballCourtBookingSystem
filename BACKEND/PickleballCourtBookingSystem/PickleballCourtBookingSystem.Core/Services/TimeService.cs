using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class TimeService : ITimeService
{
    public DateTime GetCurrentTime()
    {
        var timeZoneInfo = TimeZoneInfo.Local;
        return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
    }
}