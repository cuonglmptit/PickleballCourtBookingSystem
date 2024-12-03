using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

public interface IRoleRepository : IBaseRepository<Role>
{
    public Role? GetRoleByUser(User user);
    public Role? GetById(int roleId);
}