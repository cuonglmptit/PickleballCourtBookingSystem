using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtOwnerService : BaseService<CourtOwner>, ICourtOwnerService
{
    private readonly ICourtOwnerRepository _courtOwnerRepository;
    public CourtOwnerService(ICourtOwnerRepository repository, ICourtOwnerRepository courtOwnerRepository) : base(repository)
    {
        _courtOwnerRepository = courtOwnerRepository;
    }
    public ServiceResult GetCourtOwnerByUserIdService(Guid? userId)
    {
        if (userId == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "User id is null");
        }
        var courtOwner = _courtOwnerRepository.FindFirstByColumnValue(userId.ToString(), "userId");
        if (courtOwner != null)
        {
            return CreateServiceResult(Success: true, StatusCode: 200, Data: courtOwner);
        }
        return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "CourtOwner not found", DevMsg: "CourtOwner not found");
    }
}