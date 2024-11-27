using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtOwnerService : BaseService<CourtOwner>, ICourtOwnerService
{
    public CourtOwnerService(ICourtOwnerRepository repository) : base(repository)
    {
        
    }
}