using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtService : IBaseService<Court>
{
    public ServiceResult GetCourtForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime);
    public ServiceResult GetAvailableCourtsForTime(DateTime date, TimeSpan startTime, TimeSpan endTime);
    public ServiceResult GetCourtsByCourtClusterId(Guid courtClusterId);
}