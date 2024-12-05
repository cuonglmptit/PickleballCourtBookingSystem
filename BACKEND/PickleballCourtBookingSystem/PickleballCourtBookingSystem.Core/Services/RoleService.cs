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

    public ServiceResult GetUserRoleByUserId(Guid? userId)
    {
        if (userId == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "userId khong duoc null",
                DevMsg: "user id khong duoc null");
        }
        var user = _userRepository.GetById(userId.Value);
        Console.WriteLine(userId);
        if (user == null)
        {
            return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Invalid user", DevMsg: "Invalid user");
        }
        var roleName = _roleRepository.GetById(user.RoleId);
        return roleName == null
            ? CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Loi role cua user", DevMsg: "User co role bi sai")
            : CreateServiceResult(Success: true, StatusCode: 200, Data: roleName);
    }
}