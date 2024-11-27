using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(IDbContext context) : base(context)
    {
        
    }
}