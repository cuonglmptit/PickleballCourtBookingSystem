using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AdminService : BaseService<Admin>, IAdminService
{
    public AdminService(IAdminRepository repository) : base(repository)
    {
        
    }
}