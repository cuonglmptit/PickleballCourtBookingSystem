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

        public IEnumerable<CourtCluster> SearchCourtClusterWithFilters(string? cityName, string? courtClusterName,
            DateTime date, TimeSpan startTime, TimeSpan endTime,
            int pageSize, int pageIndex, string orderByColumn, bool DESC = false)
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
                                AND (@courtClusterName IS NULL OR cc.name LIKE @courtClusterKeyword)
                                ";


            var parameter = new DynamicParameters();

            if (!string.IsNullOrEmpty(orderByColumn))
            {
                sqlCommand += $" ORDER BY {orderByColumn} {(DESC ? "DESC" : "ASC")}";
            }

            // Kiểm tra phân trang và thêm LIMIT vào câu lệnh SQL
            if (pageSize > 0 && pageIndex > 0)
            {
                var offset = pageSize * (pageIndex - 1);
                sqlCommand += " LIMIT @pageSize OFFSET @offset";
                parameter.Add("@offset", pageSize * (pageIndex - 1)); // Set offset here
                parameter.Add("@pageSize", pageSize);
            }
            else if (pageSize > 0 || pageIndex > 0)
            {
                Console.WriteLine("SearchCourtClusterWithFilters CourtClusterRepo: Warning: Pagination skipped because either pageSize or pageIndex is missing.");
            }

            parameter.Add("@date", date);
            parameter.Add("@cityName", cityName);
            parameter.Add("@courtClusterName", courtClusterName);
            parameter.Add("@cityKeyword", cityKeyword);
            parameter.Add("@courtClusterKeyword", courtClusterKeyword);
            parameter.Add("@startTime", startTime);
            parameter.Add("@endTime", endTime);
            //Console.WriteLine(sqlCommand);
            var result = dbContext.Connection.Query<CourtCluster>(sql: sqlCommand, param: parameter);
            return result;
        }
    }
}