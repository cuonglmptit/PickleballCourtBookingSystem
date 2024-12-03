using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {

        }
    }
}
