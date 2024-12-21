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

            public User? CheckLogin(string username, string password)
            {
                var sqlCommand = $"SELECT * FROM {className} WHERE username = @username AND password = @password";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@password", password);
                var result = dbContext.Connection.QueryFirstOrDefault<User>(sqlCommand, parameters);
                return result;
            }
            
            public User? FindUserByUniqueAttribute(string username, string phoneNumber, string email)
            {
                var sqlCommand = $"SELECT * FROM {className} WHERE (username = @username OR phone_number = @phoneNumber OR email = @email)";
                var parameters = new DynamicParameters();
                parameters.Add("@username", username);
                parameters.Add("@phoneNumber", phoneNumber);
                parameters.Add("@email", email);
            
                var result = dbContext.Connection.QueryFirstOrDefault<User>(sqlCommand, parameters);
                return result;
            }
        }
    }