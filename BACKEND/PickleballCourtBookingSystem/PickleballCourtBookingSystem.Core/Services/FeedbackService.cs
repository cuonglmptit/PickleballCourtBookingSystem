using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class FeedbackService : BaseService<Feedback>, IFeedbackService
{
    public FeedbackService(IFeedbackRepository repository) : base(repository)
    {
        
    }
}