using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(IDbContext dbContext) : base(dbContext)
    {
    }
    
}