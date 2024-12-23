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
    public IEnumerable<CourtTimeSlot> FindAvailableCourtTimeSlotsByCourtId(Guid? courtId, DateTime date, TimeSpan time)
    {
        var sqlCommand = $"SELECT * FROM {className} WHERE courtId = @courtId AND isAvailable = 1 AND (date > @date OR (date = @date AND time > @time))";
        var parameters = new DynamicParameters();
        parameters.Add("@courtId", courtId);
        parameters.Add("@date", date);
        parameters.Add("@time", time);
        var result = dbContext.Connection.Query<CourtTimeSlot>(sql: sqlCommand, param: parameters);
        return result;
    }
    
    public IEnumerable<CourtTimeSlot> FindAvailableCourtTimeSlotsForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        var sqlCommand = $"SELECT * FROM {className} WHERE isAvailable = 1 AND date = @date AND time >= @startTime AND time < @endTime ORDER BY time";
        var parameters = new DynamicParameters();
        parameters.Add("@date", date);
        parameters.Add("@startTime", startTime);
        parameters.Add("@endTime", endTime);
        var result = dbContext.Connection.Query<CourtTimeSlot>(sql: sqlCommand, param: parameters);
        return result;
    }

    public IEnumerable<CourtTimeSlot> FindCourtTimeSlotsByCourtId(Guid courtId, DateTime date, TimeSpan time)
    {
        var sqlCommand = $"SELECT * FROM {className} WHERE courtId = @courtId AND (date > @date OR (date = @date AND time > @time))";
        var parameters = new DynamicParameters();
        parameters.Add("@courtId", courtId);
        parameters.Add("@date", date);
        parameters.Add("@time", time);
        var result = dbContext.Connection.Query<CourtTimeSlot>(sql: sqlCommand, param: parameters);
        return result;
    }

    public IEnumerable<CourtTimeSlot> FindAvailableCourtTimeSlotByCourtIdAndDate(Guid courtId, DateTime date, TimeSpan time)
    {
        var sqlCommand = $"SELECT * FROM {className} WHERE courtId = @courtId AND date = @date AND time > @time AND isAvailable = 1 ORDER BY time";
        var parameters = new DynamicParameters();
        parameters.Add("@courtId", courtId);
        parameters.Add("@date", date);
        parameters.Add("@time", time);
        var result = dbContext.Connection.Query<CourtTimeSlot>(sql: sqlCommand, param: parameters);
        return result;
    }
}