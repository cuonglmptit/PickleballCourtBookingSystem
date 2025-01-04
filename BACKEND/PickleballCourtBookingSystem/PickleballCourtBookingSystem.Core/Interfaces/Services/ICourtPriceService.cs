using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtPriceService : IBaseService<CourtPrice>
{
    public ServiceResult GetCourtPricesByCourtClusterId(Guid courtClusterId);

    /// <summary>
    /// Cập nhật default price (xóa các price cũ và thay bằng price mới)
    /// </summary>
    /// <param name="courtPrices"></param>
    /// <returns></returns>
    ServiceResult ModifyListCourtPriceService(List<CourtPrice> courtPrices);
}