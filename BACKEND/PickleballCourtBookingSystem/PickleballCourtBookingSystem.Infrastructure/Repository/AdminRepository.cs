using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class AdminRepository : BaseRepository<Admin>, IAdminRepository
{
    public AdminRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}