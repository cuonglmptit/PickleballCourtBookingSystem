using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICustomerService : IBaseService<Customer>
{
    public ServiceResult GetCustomerByUserIdService(Guid? userId);
}