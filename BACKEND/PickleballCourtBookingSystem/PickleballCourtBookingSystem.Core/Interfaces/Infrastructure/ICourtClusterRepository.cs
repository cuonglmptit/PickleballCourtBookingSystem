using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure
{
    public interface ICourtClusterRepository : IBaseRepository<CourtCluster>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityName"></param>
        /// <param name="courtClusterName"></param>
        /// <param name="date"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="orderBy">Cột cần order</param>
        /// <returns></returns>
        IEnumerable<CourtCluster> SearchCourtClusterWithFilters(string? cityName, string? courtClusterName,
            DateTime date, TimeSpan startTime, TimeSpan endTime,
            int pageSize, int pageIndex, string orderByColumn, bool DESC = false);

        public IEnumerable<CourtCluster> GetActiveCourtClusters();
    }

}