using PickleballCourtBookingSystem.Core.Interfaces.DBContext;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;

namespace PickleballCourtBookingSystem.Infrastructure.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    #region Field

    /// <summary>
    /// dbContext để kết nối
    /// </summary>
    protected IDbContext dbContext;

    /// <summary>
    /// Tên của class
    /// </summary>
    protected string className = typeof(T).Name;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor của BaseRepository
    /// </summary>
    /// <param name="dbContext">Kết nối đến database (đã được câu hình DI)</param>
    public BaseRepository(IDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #endregion

    #region Methods

    public virtual int Delete(Guid entityId)
    {
        return dbContext.Delete<T>(entityId);
    }

    public virtual int DeleteAny(List<Guid> ids)
    {
        return dbContext.DeleteAny<T>(ids);
    }

    public virtual IEnumerable<T> SearchByKeyword(string? keyword, string columnName)
    {
        return dbContext.SearchByKeyword<T>(keyword, columnName);
    }

    public virtual IEnumerable<T> SearchByKeywordMultipleColumns(string? keyword, List<string> columnName)
    {
        return dbContext.SearchByKeywordMultipleColumns<T>(keyword, columnName);
    }

    public virtual IEnumerable<T> FindByColumnValue(object? value, string columnName)
    {
        return dbContext.FindByColumnValue<T>(value, columnName);
    }
    public virtual T? FindFirstByColumnValue(string? keyword, string columnName)
    {
        return dbContext.FindFirstByColumnvalue<T>(keyword, columnName);
    }

    public virtual string? FindLargestValueEndsWithNumberInColumn(string columnName)
    {
        return dbContext.FindLargestValueEndsWithNumberInColumn<T>(columnName);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return dbContext.GetAll<T>();
    }

    public virtual IEnumerable<T> GetAllByColumn(string columnName, bool DESC = false)
    {
        return dbContext.GetAllOrderByColumn<T>(columnName, DESC);
    }

    public virtual T? GetById(Guid? entityId)
    {
        return dbContext.GetById<T>(entityId);
    }

    public IEnumerable<T> GetPaging(int pageSize, int pageIndex, string orderByColumn, bool DESC = false)
    {
        return dbContext.GetPaging<T>(pageSize, pageIndex, orderByColumn, DESC);
    }

    public virtual int Insert(T entity)
    {
        return dbContext.Insert(entity);
    }

    public int InsertMany(List<T> entities)
    {
        return dbContext.InsertMany(entities);
    }

    public virtual bool IsUniqueValueExistsInColumn(T entity, string columnName)
    {
        return dbContext.IsUniqueValueExistsInColumn<T>(entity, columnName);
    }

    public virtual int Update(T entity, Guid entityId)
    {
        return dbContext.Update<T>(entity, entityId);
    }

    #endregion
}