using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;

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

        /// <summary>
        /// Update các cột trong danh sách (chỉ update những cột được liệt kê trong danh sách)
        /// </summary>
        /// <param name="entity">Thực thể update</param>
        /// <param name="entityId">Id của thực thể</param>
        /// <param name="columns">Các cột muốn update</param>
        /// <returns>Kết quả service</returns>
        ServiceResult UpdateSpecifiedColumnsService(T entity, Guid entityId, List<string> columns);

        /// <summary>
        /// Lấy các bản ghi dựa trên nhiều điều kiện cột.
        /// Author: CuongLM (25/12/2024) 
        /// </summary>
        /// <param name="conditions">Danh sách các điều kiện (cột và giá trị tương ứng).</param>
        /// <returns>Danh sách các thực thể phù hợp với điều kiện; danh sách rỗng nếu không có bản ghi nào phù hợp.</returns>
        ServiceResult GetByMultipleConditionsService(Dictionary<string, object> conditions);
        #endregion
    }
}