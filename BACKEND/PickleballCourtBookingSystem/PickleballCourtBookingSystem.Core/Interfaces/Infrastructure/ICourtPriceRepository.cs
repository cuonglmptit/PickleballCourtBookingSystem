using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

public interface ICourtPriceRepository : IBaseRepository<CourtPrice>
{
    public IEnumerable<CourtPrice> GetCourtPricesByCourtClusterId(Guid courtClusterId);
}