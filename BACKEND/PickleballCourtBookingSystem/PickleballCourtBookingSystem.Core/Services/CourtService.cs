using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtService : BaseService<Court>, ICourtService
{
    public CourtService(ICourtRepository repository) : base(repository)
    {
        
    }
}