using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CustomerService : BaseService<Customer>, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository repository, ICustomerRepository customerRepository) : base(repository)
    {
        _customerRepository = customerRepository;
    }

    public ServiceResult GetCustomerByUserIdService(Guid? userId)
    {
        if (userId == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "User id is null");
        }
        var customer = _customerRepository.FindFirstByColumnValue(userId, "userId");
        if (customer != null)
        {
            return CreateServiceResult(Success: true, StatusCode: 200, Data: customer);
        }
        return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Customer not found", DevMsg: "Customer not found");
    }
}