using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    
    private readonly IDbContext _dbContext;
    public RoleRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public Role? GetRoleByUser(User user)
    {
        var roleId = user.RoleId;
        return _dbContext.FindFirstByColumnvalue<Role>(roleId, "Id");
    }

    public Role? GetById(int roleId)
    {
        return _dbContext.FindFirstByColumnvalue<Role>(roleId, "Id");
    }
}