using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services

{
public class CourtClusterService : BaseService<CourtCluster>, ICourtClusterService
{
    public CourtClusterService(IBaseRepository<CourtCluster> repository): base(repository)
    {
        
    }
    
}
}