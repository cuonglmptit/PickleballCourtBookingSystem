using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class CourtPriceRepository : BaseRepository<CourtPrice>, ICourtPriceRepository
{
    public CourtPriceRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }

    public IEnumerable<CourtPrice> GetCourtPricesByCourtClusterId(Guid courtClusterId)
    {
        return dbContext.FindByColumnValue<CourtPrice>(courtClusterId, "courtClusterId");
    }
}