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
            var listCourtPrice = _courtPriceRepository.GetByMultipleConditions(new Dictionary<string, object>
            {
                { nameof(CourtPrice.CourtClusterId), courtClusterId }
            },
            new Dictionary<string, string>
            {
                { nameof(CourtPrice.Time), "ASC" }
            });
            return CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Lấy CourtPrice thành công", DevMsg: "Lấy CourtPrice thành công", Data: listCourtPrice);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }

    public ServiceResult ModifyListCourtPriceService(List<CourtPrice> courtPrices)
    {
        try
        {
            //Lấy ra các price cũ mà có CourtPrice.Time trùng với các CourtPrice.Time trong courtPrices
            foreach (var price in courtPrices)
            {
                //Lấy ra các price cũ mà có CourtPrice.Time trùng với price.Time
                var oldPrice = _courtPriceRepository.GetByMultipleConditions(new Dictionary<string, object>
                {
                    { nameof(CourtPrice.CourtClusterId), price.CourtClusterId },
                });
                //Nếu có price cũ thì xóa
                if (oldPrice != null)
                {
                    //Truyền vào DeleteAny 1 list id của oldPrice
                    _courtPriceRepository.DeleteAny(oldPrice.Select(x => x.Id.Value).ToList());
                }
                price.Id = Guid.NewGuid();
            }
            //Thêm mới các price mới
            var result = base.InsertManyService(courtPrices);
            if (result.Success)
            {
                return CreateServiceResult(Success: true, StatusCode: 200, Data: courtPrices);
            }
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Có lỗi khi tạo giá cho sân", DevMsg: result.DevMsg);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }
}