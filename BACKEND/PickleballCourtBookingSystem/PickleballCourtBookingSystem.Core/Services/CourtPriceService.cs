using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtPriceService : BaseService<CourtPrice>, ICourtPriceService
{
    public CourtPriceService(ICourtPriceRepository repository) : base(repository)
    {
        
    }
}