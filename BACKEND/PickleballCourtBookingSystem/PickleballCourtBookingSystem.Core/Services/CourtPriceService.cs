using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtPriceService : BaseService<CourtPrice>, ICourtPriceService
{
    
    private readonly ICourtPriceRepository _courtPriceRepository;
    public CourtPriceService(ICourtPriceRepository repository, ICourtPriceRepository courtPriceRepository) : base(repository)
    {
        _courtPriceRepository = courtPriceRepository;
    }

    public ServiceResult GetCourtPricesByCourtClusterId(Guid courtClusterId)
    {
        try
        {
            var listCourtPrice = _courtPriceRepository.GetCourtPricesByCourtClusterId(courtClusterId);
            return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtPrice);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }
}