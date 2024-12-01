using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IAddressService : IBaseService<Address>
{
    public ServiceResult SearchAddress(string query);
}