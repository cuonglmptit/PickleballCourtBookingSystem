using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class RoleService : BaseService<Role>, IRoleService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    
    public RoleService(IUserRepository userRepository, IRoleRepository roleRepository) : base(roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public ServiceResult GetUserRoleByUser(User? user)
    {
        if (user == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "User la null", DevMsg: "User la null");
        }
        var roleName = _roleRepository.GetRoleByUser(user);
        if (roleName == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "User khong co role hoac user khong ton tai", DevMsg: "User khong co role hoac user khong ton tai");
        }

        return CreateServiceResult(Success: true, StatusCode: 200, Data: roleName);
    }

    public ServiceResult GetUserRoleByUserId(Guid? userId)
    {
        var user = _userRepository.GetById(userId);
        if (user == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Invalid user", DevMsg: "Invalid user");
        }
        var roleName = _roleRepository.GetRoleByUser(user);
        return roleName == null
            ? CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Loi role cua user", DevMsg: "User co role bi sai")
            : CreateServiceResult(Success: true, StatusCode: 200, Data: roleName);
    }
}