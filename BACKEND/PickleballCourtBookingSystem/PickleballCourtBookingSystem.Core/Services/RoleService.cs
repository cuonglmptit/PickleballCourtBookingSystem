using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class RoleService : BaseService<Role>, IRoleService
{
    public RoleService(IRoleRepository repository) : base(repository)
    {
        
    }
}