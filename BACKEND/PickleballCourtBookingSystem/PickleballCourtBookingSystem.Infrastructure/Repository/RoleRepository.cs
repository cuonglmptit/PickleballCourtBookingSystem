using Dapper;
using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public Role? GetById(int? roleId)
    {
        // return _dbContext.FindFirstByColumnvalue<Role>(roleId.ToString(), "Id");
        

        //Tạo câu lệnh SQL
        var sqlCommand = $"SELECT * FROM {className} WHERE Id = @entityId";

        //Tạo dynamic param và add param
        var parameters = new DynamicParameters();
        parameters.Add("@entityId", roleId);

        //Thực thi câu lệnh
        var res = dbContext.Connection.QueryFirstOrDefault<Role>(sql: sqlCommand, param: parameters);

        //Trả về kết quả
        return res;
    }
}