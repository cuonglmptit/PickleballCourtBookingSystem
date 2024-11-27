using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class AdminService : BaseService<Admin>, IAdminService
{
    public AdminService(IBaseRepository<Admin> repository) : base(repository)
    {
        
    }
}