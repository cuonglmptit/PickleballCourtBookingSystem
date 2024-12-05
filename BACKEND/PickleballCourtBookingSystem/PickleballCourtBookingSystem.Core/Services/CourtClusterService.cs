using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services

{
public class CourtClusterService : BaseService<CourtCluster>, ICourtClusterService
{
    private readonly ICourtClusterRepository _courtClusterRepository;
    private readonly ICourtService _courtService;
    public CourtClusterService(ICourtClusterRepository repository, ICourtClusterRepository courtClusterRepository, ICourtService courtService): base(repository)
    {
        _courtClusterRepository = courtClusterRepository;
        _courtService = courtService;
    }

    public ServiceResult GetCourtClustersForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            var listCourt = (List<Court>) _courtService.GetCourtForTimeRange(date, startTime, endTime).Data!;
            var listCourtCluster = new List<CourtCluster>();
            var courtClusterIds = new HashSet<Guid>();
            foreach (var court in listCourt)
            {
                if (court.CourtClusterId == null)
                {
                    return CreateServiceResult(false, StatusCode: 500, UserMsg: "id bi null", DevMsg: "court cluster id trong bang court la null");
                }

                courtClusterIds.Add(court.CourtClusterId.Value);
            }
            foreach (var courtClusterId in courtClusterIds)
            {
                var courtCluster = _courtClusterRepository.GetById(courtClusterId);
                
                if (courtCluster == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court cluster not found",
                        DevMsg: "Court cluster not found");
                }
                listCourtCluster.Add(courtCluster);
            }
            return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtCluster);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }
    
    public ServiceResult GetAvailableCourtClusterForTime(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            var listCourt = (List<Court>) _courtService.GetAvailableCourtsForTime(date, startTime, endTime).Data!;
            var listCourtCluster = new List<CourtCluster>();
            var courtClusterIds = new HashSet<Guid>();
            foreach (var court in listCourt)
            {
                if (court.CourtClusterId == null)
                {
                    return CreateServiceResult(false, StatusCode: 500, UserMsg: "id bi null", DevMsg: "court cluster id trong bang court la null");
                }

                courtClusterIds.Add(court.CourtClusterId.Value);
            }
            foreach (var courtClusterId in courtClusterIds)
            {
                var courtCluster = _courtClusterRepository.GetById(courtClusterId);
                
                if (courtCluster == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court cluster not found",
                        DevMsg: "Court cluster not found");
                }
                listCourtCluster.Add(courtCluster);
            }
            return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtCluster);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }
}
}