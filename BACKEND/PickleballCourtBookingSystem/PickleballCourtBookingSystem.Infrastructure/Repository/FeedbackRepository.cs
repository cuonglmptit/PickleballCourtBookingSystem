using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class FeedbackRepository : BaseRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}