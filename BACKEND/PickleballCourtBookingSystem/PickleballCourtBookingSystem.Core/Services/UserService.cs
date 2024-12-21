using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class UserService : BaseService<User>, IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository repository, IUserRepository userRepository) : base(repository)
    {
        _userRepository = userRepository;
    }
}