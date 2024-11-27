using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class CourtOwnerRepository : BaseRepository<CourtOwner>, ICourtOwnerRepository
{
    public CourtOwnerRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}