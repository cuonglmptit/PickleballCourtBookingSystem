using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

public interface IBookingRepository : IBaseRepository<Booking>
{
    public IEnumerable<Booking> GetCompletedBookingByDate(Guid courtClusterId, DateTime startDate, DateTime endDate);
}