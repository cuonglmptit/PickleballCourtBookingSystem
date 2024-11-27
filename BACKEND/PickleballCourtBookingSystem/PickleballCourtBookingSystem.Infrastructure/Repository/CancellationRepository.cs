using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class CancellationRepository : BaseRepository<Cancellation>, ICancellationRepository
{
    public CancellationRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}