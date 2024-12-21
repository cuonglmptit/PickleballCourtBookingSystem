using PickleballCourtBookingSystem.Core.DTOs;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services

{
    /// <summary>
    /// Interface service base
    /// </summary>
    /// <typeparam name="T">Class muốn làm việc</typeparam>
    public interface IBaseService<T> where T : class
    {
        #region Methods

        /// <summary>
        /// Insert 1 bản ghi vào databse
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ServiceResult với Sucess = true nếu xóa thành công và ngược lại nếu thất bại</returns>
        ServiceResult InsertService(T entity);
        ServiceResult InsertManyService(List<T> entities);
        /// <summary>
        /// Update 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi muốn cập nhật</param>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>ServiceResult với Sucess = true nếu xóa thành công và ngược lại nếu thất bại</returns>
        ServiceResult UpdateService(T entity, Guid id);
        ServiceResult UpdatePartialService(T entity, Guid id);
        
        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="entityId">Id của bản ghi muốn xóa</param>
        /// <returns>ServiceResult với Sucess = true nếu xóa thành công và ngược lại nếu thất bại</returns>
        ServiceResult DeleteService(Guid entityId);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Dãy các employee id</param>
        /// <returns>ServiceResult với Sucess = true nếu xóa thành công và ngược lại nếu thất bại</returns>
        ServiceResult DeleteAnyService(List<Guid> ids);
        
        ServiceResult GetAllService();
        
        ServiceResult GetByIdService(Guid id);
        
        #endregion
    }
}