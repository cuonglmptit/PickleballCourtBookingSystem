using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository
{
    public class CourtClusterRepository : BaseRepository<CourtCluster>, ICourtClusterRepository
    {
        public CourtClusterRepository(IDbContext dbContext) : base(dbContext)
        {

        }
    }
}