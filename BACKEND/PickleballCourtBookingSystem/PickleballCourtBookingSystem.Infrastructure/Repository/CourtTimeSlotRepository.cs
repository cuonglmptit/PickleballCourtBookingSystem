using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class CourtTimeSlotRepository : BaseRepository<CourtTimeSlot>, ICourtTimeSlotRepository
{
    public CourtTimeSlotRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}