using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class ImageCourtUrlService : BaseService<ImageCourtUrl>, IImageCourtUrlService
{
    
    private readonly IImageCourtUrlRepository _imageCourtUrlRepository;
    public ImageCourtUrlService(IImageCourtUrlRepository repository, IImageCourtUrlRepository imageCourtUrlRepository) : base(repository)
    {
        _imageCourtUrlRepository = imageCourtUrlRepository;
    }

    public ServiceResult GetImageCourtUrlService(Guid courtClusterId)
    {
        try
        {
            var result = _imageCourtUrlRepository.FindByColumnValue(courtClusterId, "courtClusterId");
            
            return CreateServiceResult(
                Success: true,
                StatusCode: 200,
                UserMsg: "Lấy danh sách hình ảnh thành công",
                Data: result
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Lỗi khi lấy danh sách cụm sân", DevMsg: e.Message);
        }
        
    }
}