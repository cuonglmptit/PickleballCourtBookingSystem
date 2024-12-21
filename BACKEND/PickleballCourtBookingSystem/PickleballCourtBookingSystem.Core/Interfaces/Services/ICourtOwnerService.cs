using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface ICourtOwnerService : IBaseService<CourtOwner>
{
    public ServiceResult GetCourtOwnerByUserIdService(Guid? userId);
}