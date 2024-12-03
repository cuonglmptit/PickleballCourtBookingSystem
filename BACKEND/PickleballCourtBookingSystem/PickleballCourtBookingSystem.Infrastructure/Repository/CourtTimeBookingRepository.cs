using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class CourtTimeBookingRepository : BaseRepository<CourtTimeBooking>, ICourtTimeBookingRepository
{
    public CourtTimeBookingRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}