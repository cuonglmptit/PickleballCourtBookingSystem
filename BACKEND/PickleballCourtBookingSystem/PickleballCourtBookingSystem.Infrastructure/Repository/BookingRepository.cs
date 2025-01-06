using Dapper;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class BookingRepository : BaseRepository<Booking>, IBookingRepository
{
    
    public BookingRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }

    public IEnumerable<Booking> GetCompletedBookingByDate(Guid courtClusterId, DateTime startDate, DateTime endDate)
    {
        var sqlCommand = $"SELECT * FROM {className} WHERE courtClusterId = @courtClusterId AND timeBooking >= @startDate AND timeBooking <= @endDate AND paymentStatus = 1";
        var parameters = new DynamicParameters();
        parameters.Add("@startDate", startDate);
        parameters.Add("@endDate", endDate);
        parameters.Add("courtClusterId", courtClusterId);
        var result = dbContext.Connection.Query<Booking>(sql: sqlCommand, param: parameters);
        return result;
    }
    
    public IEnumerable<Booking> GetAllCompletedBooking(Guid courtClusterId)
    {
        var sqlCommand = $"SELECT * FROM {className} WHERE courtClusterId = @courtClusterId AND timeBooking >= @startDate AND timeBooking <= @endDate AND paymentStatus = 1";
        var parameters = new DynamicParameters();
        parameters.Add("courtClusterId", courtClusterId);
        var result = dbContext.Connection.Query<Booking>(sql: sqlCommand, param: parameters);
        return result;
    }
}