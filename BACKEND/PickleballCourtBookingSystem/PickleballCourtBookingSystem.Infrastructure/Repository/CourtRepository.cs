using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Infrastructure.Repository
{
    public class CourtRepository : BaseRepository<Court>, ICourtRepository
    {
        public CourtRepository(IDbContext dbContext) : base(dbContext)
        {

        }
        
        public IEnumerable<Court> GetCoursByCourtClusterId(Guid courtClusterId)
        {
            return dbContext.FindByColumnValue<Court>(courtClusterId, "courtClusterId");
        }
    }
}
