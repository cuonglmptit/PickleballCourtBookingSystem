using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure
{
    public interface ICourtClusterRepository : IBaseRepository<CourtCluster>
    {
        public IEnumerable<CourtCluster> SearchCourtClusterWithFilters(string? cityName, string? courtClusterName,
            DateTime date, TimeSpan startTime, TimeSpan endTime);
    }
    
}