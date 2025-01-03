using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IImageCourtUrlService : IBaseService<ImageCourtUrl>
{
    public ServiceResult GetImageCourtUrlService(Guid courtClusterId);
}