using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class BookingService : BaseService<Booking>, IBookingService
{
    public BookingService(IBookingRepository repository) : base(repository)
    {
        
    }
}