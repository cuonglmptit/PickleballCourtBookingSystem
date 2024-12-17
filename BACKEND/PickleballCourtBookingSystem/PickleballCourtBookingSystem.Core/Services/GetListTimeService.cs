using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class GetListTimeService : IGetListTimeService
{
    public ServiceResult GetListTime()
    {
        var timeList = new List<string>(); // Sửa thành List<string> để chỉ trả về chuỗi "hh:mm"
        foreach (TimeEnum time in Enum.GetValues(typeof(TimeEnum)))
        {
            var timeSpan = TimeSpan.FromHours((int)time);
            timeList.Add(timeSpan.ToString(@"hh\:mm")); // Định dạng TimeSpan thành "hh:mm"
        }
        return new ServiceResult
        {
            Success = true,
            StatusCode = 200,
            Data = timeList
        };
    }
}
