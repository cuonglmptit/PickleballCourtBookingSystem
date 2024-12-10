using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class GetListTimeService : IGetListTimeService
{
    public ServiceResult GetListTime()
    {
        var timeList = new List<TimeSpan>();
        foreach (TimeEnum time in Enum.GetValues(typeof(TimeEnum)))
        {
            timeList.Add(TimeSpan.FromHours((int)time));
        }
        return new ServiceResult
        {
            Success = true,
            StatusCode = 200,
            Data = timeList
        };
    }
}
