using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IRoleService : IBaseService<Role>
{
    public ServiceResult GetUserRoleByUserId(Guid? userId);
}