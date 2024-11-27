using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class RoleService : BaseService<Role>, IRoleService
{
    public RoleService(IBaseRepository<Role> repository) : base(repository)
    {
        
    }
}