using PickleballCourtBookingSystem.Api.Models;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

public interface IRoleRepository : IBaseRepository<Role>
{
    public Role? GetRoleByUser(User user);
    public Role? GetById(int roleId);
}