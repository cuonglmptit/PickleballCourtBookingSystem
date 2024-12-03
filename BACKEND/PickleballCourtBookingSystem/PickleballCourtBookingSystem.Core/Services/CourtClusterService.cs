using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services

{
public class CourtClusterService : BaseService<CourtCluster>, ICourtClusterService
{
    private readonly ICourtClusterRepository _courtClusterRepository;
    public CourtClusterService(ICourtClusterRepository repository, ICourtClusterRepository courtClusterRepository): base(repository)
    {
        _courtClusterRepository = courtClusterRepository;
    }
    
}
}