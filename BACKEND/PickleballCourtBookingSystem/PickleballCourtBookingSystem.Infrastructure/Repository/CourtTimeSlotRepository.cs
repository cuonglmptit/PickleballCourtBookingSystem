using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class CourtTimeSlotRepository : BaseRepository<CourtTimeSlot>, ICourtTimeSlotRepository
{
    public CourtTimeSlotRepository(IDbContext dbContext) : base(dbContext)
    {
    }
    public IEnumerable<CourtTimeSlot> FindCourtTimeSlots(Guid? courtId)
    {
        var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
        var date = timeNow.Date;
        var time = timeNow.TimeOfDay;
        var sqlCommand = $"SELECT * FROM {className} WHERE courtId = @courtId AND isAvailable = 1 AND (date > @date OR (date = @date AND time > @time))";
        var parameters = new DynamicParameters();
        parameters.Add("@courtId", courtId);
        parameters.Add("@date", date);
        parameters.Add("@time", time);
        var result = dbContext.Connection.Query<CourtTimeSlot>(sql: sqlCommand, param: parameters);
        return result;
    }

}