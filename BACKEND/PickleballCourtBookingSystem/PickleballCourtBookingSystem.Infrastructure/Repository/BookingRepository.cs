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
}