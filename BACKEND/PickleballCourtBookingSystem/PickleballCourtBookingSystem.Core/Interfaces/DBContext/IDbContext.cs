using System.Data;

namespace PickleballCourtBookingSystem.Core.Interfaces.DBContext

{
    /// <summary>
    /// Interface làm việc với Database
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Connection của db tương ứng
        /// </summary>
        public IDbConnection Connection { get; }

        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <returns>Tất cả các bản ghi, danh sách trống nếu không có bản ghi</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// Lấy ra bản thi theo id
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Thực thể của class muốn lấy, null - Nếu không có</returns>
        T? GetById<T>(Guid? entityId);

        /// <summary>
        /// Insert dữ liệu
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <param name="entity">Thực thể muốn thêm vào database</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        int Insert<T>(T entity);

        int InsertMany<T>(List<T> entities);
        /// <summary>
        /// Update dữ liệu
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <param name="entity">Thực thể muốn xóa</param>
        /// <param name="entityId">Id của thực thể muốn update</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        int Update<T>(T entity, Guid entityId);

        /// <summary>
        /// Delete dữ liệu
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <param name="entityId">Id của bản ghi muốn xóa</param>
        /// <returns>Số bản ghi đã xóa</returns>
        int Delete<T>(Guid entityId);

        /// <summary>
        /// Delete nhiều bản ghi
        /// Author; CuongLM (07/08/2024)
        /// </summary>
        /// <param name="ids">List Guid danh sách các bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        int DeleteAny<T>(List<Guid> ids);

        /// <summary>
        /// Check giá trị tồn tại duy nhất trong cột (không một bản ghi nào có id khác bản ghi của giá trị mà có giá trị giống)
        /// nghĩa là nếu có bản ghi khác id mà lại có giá trị trùng thì false
        /// Lệnh sẽ dạng này: WHERE Code = @Code AND Id != @Id
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">thực thể muốn kiểm tra</param>
        /// <param name="columnName">cột muốn kiểm tra</param>
        /// <returns>true-giá trị này không bị duplicate trong cột, false ngược lại</returns>
        bool IsUniqueValueExistsInColumn<T>(T entity, string columnName);

        /// <summary>
        /// Lấy ra bản ghi chứa giá trị keyword tương ứng với cột.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của bản ghi cần tìm.</typeparam>
        /// <param name="keyword">Từ khóa cần tìm trong cột.</param>
        /// <param name="columnName">Tên của cột để thực hiện tìm kiếm.</param>
        /// <returns>Thực thể của class muốn lấy; trả về null nếu không tìm thấy bản ghi nào.</returns>
        /// <remarks>
        /// Author: CuongLM (04/08/2024)
        /// </remarks>
        IEnumerable<T> SearchByKeyword<T>(string? keyword, string columnName);
        IEnumerable<T> SearchByKeywordMultipleColumns<T>(string? keyword, List<string> columnName);
        
        public IEnumerable<T> FindByColumnValue<T>(object? value, string columnName);
        T? FindFirstByColumnvalue<T>(object? keyword, string columnName);
        /// <summary>
        /// Tìm ra giá trị lớn nhất của cột mà kết thúc bằng số
        /// Author: CuongLM (08/08/2024)
        /// </summary>
        /// <param name="columnName">Tên của cột cần tìm</param>
        /// <returns>Giá trị lớn nhất, null nếu không có</returns>
        string? FindLargestValueEndsWithNumberInColumn<T>(string columnName);

        /// <summary>
        /// Lấy ra các thực thể, order theo cột được chỉ định
        /// Author: CuongLM (08/08/2024)
        /// </summary>
        /// <typeparam name="T">Tên thực thể (bảng)</typeparam>
        /// <param name="columnName">Tên cột</param>
        /// <param name="DESC">Có sắp xếp theo chiều ngược lại không, true-là DESC, false-ASC</param>
        /// <returns>Danh sách thực thể được sắp xếp theo đầu vào</returns>
        IEnumerable<T> GetAllOrderByColumn<T>(string columnName, bool DESC = false);

        /// <summary>
        /// Lấy ra phân trang của nhân viên (ORDER BY mã nhân viên)
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <param name="pageSize">Size của 1 trang</param>
        /// <param name="pageIndex">Số thứ tự của trang</param>
        /// <param name="orderByColumn">Cột muốn sắp xếp</param>
        /// <returns>Các bản ghi của trang</returns>
        IEnumerable<T> GetPaging<T>(int pageSize, int pageIndex, string orderByColumn, bool DESC = false);
        #endregion
    }
}