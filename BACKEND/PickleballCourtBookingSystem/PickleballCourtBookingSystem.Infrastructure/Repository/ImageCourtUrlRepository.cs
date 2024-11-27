using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class ImageCourtUrlRepository : BaseRepository<ImageCourtUrl>, IImageCourtUrlRepository
{
    public ImageCourtUrlRepository(IDbContext dbContext) : base(dbContext)
    {
        
    }
}