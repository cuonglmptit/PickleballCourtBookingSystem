using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AddressService : BaseService<Address>, IAddressService
{
    
    private readonly IAddressRepository _addressRepository;
    public AddressService(IAddressRepository repository) : base(repository)
    {
        _addressRepository = repository;
    }
    public ServiceResult SearchAddress(string query)
    {
        
        try
        {
            var listAddressColumn =  new List<string>{"city", "district", "ward", "street"};
            var address = _addressRepository.SearchByKeywordMultipleColumns(query, listAddressColumn);
            return CreateServiceResult(Success: true, StatusCode:200, Data: address);
        }
        catch (Exception e)
        {
            return CreateServiceResult(Success: false, StatusCode: 500, DevMsg: e.Message);
        }
    }
}