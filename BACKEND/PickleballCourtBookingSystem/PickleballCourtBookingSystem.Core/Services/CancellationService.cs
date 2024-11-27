using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CancellationService : BaseService<Cancellation>, ICancellationService
{
    public CancellationService(ICancellationRepository repository) : base(repository)
    {
        
    }
}