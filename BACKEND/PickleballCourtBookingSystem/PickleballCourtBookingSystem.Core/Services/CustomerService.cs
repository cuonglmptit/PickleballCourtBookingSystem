using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CustomerService : BaseService<Customer>, ICustomerService
{
    public CustomerService(ICustomerRepository repository) : base(repository)
    {
        
    }
}