using Dapper;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository
{
    public class CourtClusterRepository : BaseRepository<CourtCluster>, ICourtClusterRepository
    {
        public CourtClusterRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<CourtCluster> SearchCourtClusterWithFilters(string? cityName, string? courtClusterName, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var cityKeyword = "%" + cityName + "%";
            var courtClusterKeyword = "%" + courtClusterName + "%";
            var sqlCommand = $@"
                                SELECT DISTINCT cc.* 
                                FROM {className} cc
                                JOIN Address a ON cc.addressId = a.id
                                JOIN Court c ON cc.id = c.courtClusterId
                                JOIN CourtTimeSlot cts ON c.id = cts.courtId
                                WHERE cts.isAvailable = 1
                                AND cts.date = @date
                                AND cts.time >= @startTime
                                AND cts.time < @endTime
                                AND (@cityName IS NULL OR a.city LIKE @cityKeyword)
                                AND (@courtClusterName IS NULL OR cc.name LIKE @courtClusterKeyword);
                                ";

            var parameter = new DynamicParameters();
            parameter.Add("@date", date);
            parameter.Add("@cityName", cityName);
            parameter.Add("@courtClusterName", courtClusterName);
            parameter.Add("@cityKeyword", cityKeyword);
            parameter.Add("@courtClusterKeyword", courtClusterKeyword);
            parameter.Add("@startTime", startTime);
            parameter.Add("@endTime", endTime);
            var result = dbContext.Connection.Query<CourtCluster>(sql: sqlCommand, param: parameter);
            return result;
        }
    }
}