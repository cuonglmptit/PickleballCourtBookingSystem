namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure

{
    public interface IBaseRepository<T> where T : class
    {
        #region Methods
        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả các bản ghi, danh sách trống nếu không có bản ghi</returns>
        /// Author: CuongLM (04/08/2024)
        IEnumerable<T> GetAll();

        /// <summary>
        /// Lấy ra bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Thực thể của class muốn lấy, null - Nếu không có</returns>
        /// Author: CuongLM (04/08/2024)
        T? GetById(Guid entityId);

        /// <summary>
        /// Insert dữ liệu
        /// </summary>
        /// <param name="entity">Thực thể muốn thêm vào database</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author: CuongLM (04/08/2024)
        int Insert(T entity);

        int InsertMany(List<T> entities);

        /// <summary>
        /// Update dữ liệu
        /// </summary>
        /// <param name="entity">Thực thể muốn xóa</param>
        /// <param name="entityId">Id của thực thể muốn update</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author: CuongLM (04/08/2024)
        int Update(T entity, Guid entityId);

        /// <summary>
        /// Delete dữ liệu
        /// </summary>
        /// <param name="entityId">Id của bản ghi muốn xóa</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Author: CuongLM (04/08/2024)
        int Delete(Guid entityId);

        /// <summary>
        /// Delete nhiều bản ghi
        /// </summary>
        /// <param name="ids">List Guid danh sách các bản ghi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Author; CuongLM (07/08/2024)
        int DeleteAny(List<Guid> ids);

        /// <summary>
        /// Check giá trị tồn tại duy nhất trong cột (không một bản ghi nào có id khác bản ghi truyền vào mà có giá trị giống nhau ở cột truyền vào)
        /// nghĩa là nếu có bản ghi khác id mà lại có giá trị trùng thì false
        /// Lệnh sẽ dạng này: WHERE ColName = @Value AND Id != @Id
        /// Author: CuongLM (04/08/2024)
        /// </summary>
        /// <param name="entity">Thực thể muốn kiểm tra</param>
        /// <param name="columnName">cột muốn kiểm tra</param>
        /// <returns>true-giá trị này không bị duplicate trong cột, false ngược lại</returns>
        bool IsUniqueValueExistsInColumn(T entity, string columnName);

        /// <summary>
        /// Lấy ra bản ghi theo keyword tương ứng với cột
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Thực thể của class muốn lấy, null - Nếu không có</returns>
        /// Author: CuongLM (04/08/2024)
        IEnumerable<T> SearchByKeyword(string? keyword, string columnName);
        IEnumerable<T> SearchByKeywordMultipleColumns(string? keyword, List<string> columnName);
        IEnumerable<T> FindByColumnValue(object? value, string columnName);
        T? FindFirstByColumnValue(string? keyword, string columnName);
        /// <summary>
        /// Tìm ra giá trị lớn nhất của cột mà kết thúc bằng số
        /// </summary>
        /// <param name="columnName">Tên của cột cần tìm</param>
        /// <returns>Giá trị lớn nhất, null nếu không có</returns>
        string? FindLargestValueEndsWithNumberInColumn(string columnName);

        /// <summary>
        /// Lấy ra các thực thể, order theo cột được chỉ định
        /// Author: CuongLM (08/08/2024)
        /// </summary>
        /// <typeparam name="T">Tên thực thể (bảng)</typeparam>
        /// <param name="columnName">Tên cột</param>
        /// <param name="DESC">Có sắp xếp theo chiều ngược lại không, true-là DESC, false-ASC</param>
        /// <returns>Danh sách thực thể được sắp xếp theo đầu vào</returns>
        IEnumerable<T> GetAllByColumn(string columnName, bool DESC = false);

        /// <summary>
        /// Lấy ra phân trang của nhân viên (ORDER BY mã nhân viên)
        /// </summary>
        /// <param name="pageSize">Size của 1 trang</param>
        /// <param name="pageIndex">Số thứ tự của trang</param>
        /// <param name="orderByColumn">Cột muốn sắp xếp</param>
        /// <returns>Các bản ghi của trang</returns>
        /// Author: CuongLM (04/08/2024)
        IEnumerable<T> GetPaging(int pageSize, int pageIndex, string orderByColumn, bool DESC = false);

        /// <summary>
        /// Check giá trị tồn tại duy nhất trong các cột được truyền vào (không một bản ghi nào có id khác bản ghi truyền vào mà có giá trị giống nhau ở các cột truyền vào)
        /// nghĩa là nếu có bản ghi khác id mà lại có giá trị trùng ở 1 trong các cột thì false
        /// Lệnh sẽ dạng này: WHERE (ColName1 = @T.Col1Value OR ColName2 = @T.Col2Value ...) AND Id != @Id
        /// Author: CuongLM (22/12/2024)
        /// </summary>
        /// <param name="entity">Thực thể muốn kiểm tra</param>
        /// <param name="columns">Tên của các cột muốn kiểm tra</param>
        /// <returns>List các cột bị trùng nếu có, còn nếu không trùng thì là list rỗng</returns>
        public List<string> HasDuplicateValuesInOtherRecords(T entity, List<string> columns);

        #endregion
    }
}