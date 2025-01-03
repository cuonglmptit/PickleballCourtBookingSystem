using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtClusterService: IBaseService<CourtCluster>
{
    public ServiceResult GetCourtClustersForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime);
    public ServiceResult GetAvailableCourtClusterForTime(DateTime date, TimeSpan startTime, TimeSpan endTime);

    public ServiceResult AddTimeSlotsWithDefaultPrice(Guid courtClusterId, List<DateTime> dates);

    public ServiceResult SearchCourtClusterWithFiltersService(string? cityName, string? courtClusterName,
        DateTime? date, TimeSpan? startTime, TimeSpan? endTime,
        int? pageSize, int? pageIndex, string? orderByColumn, bool? DESC = false);

    /// <summary>
    /// Lấy ra các courts của một CourtCluster
    /// </summary>
    /// <param name="courtClusterId"> Id của CourtCluster </param>
    /// <returns>Các courts</returns>
    public ServiceResult GetCourtsByClusterId(Guid courtClusterId);

    public ServiceResult RegisterNewCourtCluster(Guid userId, string name, string? description, TimeSpan openingTime,
        TimeSpan closingTime, string city, string district, string ward, string street, int numberOfCourts);
    
    public ServiceResult GetCourtClusterByOwner(Guid userId);
    
    /// <summary>
    /// Lấy ra các court theo CourtOwnerId
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public ServiceResult GetCourtClusterByCourtOwner(Guid userId);

    public ServiceResult GetAllActiveCourtClusters();
    public ServiceResult GetImageUrl(Guid courtClusterId);
}