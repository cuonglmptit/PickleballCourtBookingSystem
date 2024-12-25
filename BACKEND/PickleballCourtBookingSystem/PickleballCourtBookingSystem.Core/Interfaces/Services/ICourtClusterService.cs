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
    /// Lấy ra các courts của một CourtCluster
    /// </summary>
    /// <param name="courtClusterId"> Id của CourtCluster </param>
    /// <returns>Các courts</returns>
    public ServiceResult GetCourtsByClusterId(Guid courtClusterId);
}