using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class ImageCourtUrlService : BaseService<ImageCourtUrl>, IImageCourtUrlService
{
    public ImageCourtUrlService(IBaseRepository<ImageCourtUrl> repository) : base(repository)
    {
        
    }
}