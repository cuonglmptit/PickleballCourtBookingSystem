    using System.Reflection.Metadata;
    using Dapper;
    using PickleballCourtBookingSystem.Api.Models;
    using PickleballCourtBookingSystem.Core.Entities;
    using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
    using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

    namespace PickleballCourtBookingSystem.Infrastructure.Repository
    {
        public class UserRepository : BaseRepository<User>, IUserRepository
        {
            public UserRepository(IDbContext dbContext) : base(dbContext)
            {

            }

            public User? GetUserByUsername(string username)
            {
                var sqlCommand = $"SELECT * FROM {className} WHERE username = @username";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                var result = dbContext.Connection.QueryFirstOrDefault<User>(sqlCommand, parameters);
                return result;
            }
        }
    }