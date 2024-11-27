using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtTimeBookingService : BaseService<CourtTimeBooking>, ICourtTimeBookingService
{
    public CourtTimeBookingService(ICourtTimeBookingRepository repository) : base(repository)
    {
        
    }
}