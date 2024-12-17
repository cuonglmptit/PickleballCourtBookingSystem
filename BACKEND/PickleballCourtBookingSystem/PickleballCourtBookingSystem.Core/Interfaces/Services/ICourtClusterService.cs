using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtClusterService: IBaseService<CourtCluster>
{
    public ServiceResult GetCourtClustersForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime);
    public ServiceResult GetAvailableCourtClusterForTime(DateTime date, TimeSpan startTime, TimeSpan endTime);

    public ServiceResult AddTimeSlotsWithDefaultPrice(Guid courtClusterId, List<DateTime> dates);

    public ServiceResult SearchCourtClusterWithFilters(string? cityName, string? courtClusterName,
        DateTime? date, TimeSpan? startTime, TimeSpan? endTime);

    /// <summary>
    /// CuongLM (17/12/2024)
    /// Lấy ra các sân của một cụm sân
    /// </summary>
    /// <param name="courtClusterId">Id của cụm sân</param>
    /// <returns>Các sân thuộc cụm sân</returns>
    public ServiceResult GetCourtsByClusterId(Guid courtClusterId);
}