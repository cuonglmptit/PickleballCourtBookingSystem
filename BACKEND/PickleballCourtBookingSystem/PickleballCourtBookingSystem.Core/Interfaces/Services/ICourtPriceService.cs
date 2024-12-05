using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtPriceService : IBaseService<CourtPrice>
{
    public ServiceResult GetCourtPricesByCourtClusterId(Guid courtClusterId);
}