using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtService : BaseService<Court>, ICourtService
{
    
    private readonly ICourtRepository _courtRepository;
    public CourtService(ICourtRepository repository, ICourtRepository courtRepository) : base(repository)
    {
        _courtRepository = courtRepository;
    }
}