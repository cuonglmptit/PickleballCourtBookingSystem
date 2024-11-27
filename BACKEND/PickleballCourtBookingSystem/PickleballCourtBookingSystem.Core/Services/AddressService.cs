using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AddressService : BaseService<Address>, IAddressService
{
    public AddressService(IBaseRepository<Address> repository) : base(repository)
    {
        
    }
}