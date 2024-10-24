using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;
using PickleballCourtBookingSystem.Core.CustomAttribute;

namespace PickleballCourtBookingSystem.Core.Interfaces.DBContext;

public class MySqlDbContext : IDbContext
{
        /// <summary>
        /// Connection của MySQL
        /// </summary>
        public IDbConnection Connection { get; }

        #region Constructors
        /// <summary>
        /// Constructor của MySqlContext
        /// </summary>
        /// <param name="config">Config của app, install từ package Microsoft.Extensions.Configuration</param>
        /// Author: CuongLM (07/08/2024)
        public MySqlDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("MySQL"));
        }
        #endregion

        #region Methods
        public IEnumerable<T> GetAll<T>()
        {
            var className = typeof(T).Name;

            //Tạo câu lệnh SQL
            var sqlCommand = $"SELECT * FROM {className}";
            //Thực thi câu lệnh
            var res = Connection.Query<T>(sql: sqlCommand);
            //Trả về kết quả
            return res;
        }

        public T? GetById<T>(Guid? entityId)
        {
            var className = typeof(T).Name;

            //Tạo câu lệnh SQL
            var sqlCommand = $"SELECT * FROM {className} WHERE {className}Id = @entityId";

            //Tạo dynamic param và add param
            var parameters = new DynamicParameters();
            parameters.Add("@entityId", entityId);

            //Thực thi câu lệnh
            var res = Connection.QueryFirstOrDefault<T>(sql: sqlCommand, param: parameters);

            //Trả về kết quả
            return res;
        }

        public int Insert<T>(T entity)
        {
            var className = typeof(T).Name;

            //Khai báo biến các prop/column và value của thực thể
            var propListName = String.Empty;
            var propListValue = String.Empty;

            //Tạo dynamic parameters
            var parameters = new DynamicParameters();

            //Lấy ra các property của entity
            var properties = entity.GetType().GetProperties();

            //Duyệt qua các prop và buid câu lệnh insert
            foreach (var property in properties)
            {
                //Lấy ra tên của property
                var propName = property.Name;

                //Lấy ra value của property
                var propValue = property.GetValue(entity);

                //Nếu không đánh dấu NotInQuery thì mới thêm vào
                if (!Attribute.IsDefined(property, typeof(NotInQuery)))
                {
                    propListName += $"{propName},";
                    propListValue += $"@{propName},";
                }

                //Thêm vào dynamic parameters
                parameters.Add($"@{propName}", propValue);
            }
            //Loại bỏ dấu "," cuối chuỗi
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);

            //Tạo câu lệnh sql
            var sqlCommand = $"INSERT INTO {className}({propListName}) VALUES({propListValue})";

            //Khai báo biến số bản ghi ảnh hưởng
            var res = 0;

            //Thực thi câu lệnh
            res += Connection.Execute(sql: sqlCommand, param: parameters);

            //Trả về kết quả
            return res;
        }

        public int Update<T>(T entity, Guid entityId)
        {
            var className = typeof(T).Name;

            // Khai báo biến các prop/column và value của thực thể
            var setClause = String.Empty;

            // Tạo dynamic parameters
            var parameters = new DynamicParameters();

            // Lấy ra các property của entity
            var properties = entity.GetType().GetProperties();

            // Duyệt qua các prop và build câu lệnh update
            foreach (var property in properties)
            {
                // Lấy ra tên của property
                var propName = property.Name;

                // Lấy ra value của property
                var propValue = property.GetValue(entity);

                // Nếu không đánh dấu NotInQuery thì mới thêm vào, Muốn đổi thành id khác cũng không được
                if (!Attribute.IsDefined(property, typeof(NotInQuery)) && !Attribute.IsDefined(property, typeof(PrimaryKey)))
                {
                    // Xây dựng phần SET của câu lệnh UPDATE
                    setClause += $"{propName} = @{propName}, ";

                    // Thêm vào dynamic parameters
                    parameters.Add($"@{propName}", propValue);
                }
            }

            // Loại bỏ dấu "," cuối chuỗi
            if (setClause.EndsWith(", "))
            {
                setClause = setClause.Substring(0, setClause.Length - 2);
            }

            // Thêm tham số cho entityId
            parameters.Add("@entityId", entityId);

            // Tạo câu lệnh SQL
            var sqlCommand = $"UPDATE {className} SET {setClause} WHERE {className}Id = @entityId";

            // Khai báo biến số bản ghi ảnh hưởng
            var res = 0;

            // Thực thi câu lệnh
            res += Connection.Execute(sql: sqlCommand, param: parameters);

            // Trả về kết quả
            return res;
        }


        public int Delete<T>(Guid entityId)
        {
            var className = typeof(T).Name;

            //Câu lệnh delete
            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id = @entityId";

            //Tạo dynamic param và add param
            var parameters = new DynamicParameters();
            parameters.Add("@entityId", entityId);

            //Thực thi câu lệnh
            var res = Connection.Execute(sql: sqlCommand, param: parameters);

            //Trả về kết quả
            return res;
        }

        public int DeleteAny<T>(List<Guid> ids)
        {
            var className = typeof(T).Name;

            // Khai báo biến số bản ghi ảnh hưởng
            var res = 0;

            // Tạo câu lệnh SQL và tham số
            var sqlCommand = $"DELETE FROM {className} WHERE {className}Id IN @ids";

            // Tạo dynamic parameters
            var parameters = new DynamicParameters();
            parameters.Add("@ids", ids);

            // Thực thi câu lệnh
            res = Connection.Execute(sql: sqlCommand, param: parameters);

            return res;
        }


        public void Dispose()
        {
            Connection.Dispose();
        }

        public bool IsUniqueValueExistsInColumn<T>(T entity, string columnName)
        {
            var className = typeof(T).Name;
            //Tạo câu lệnh SQL
            var sqlCheck = $"SELECT * FROM {className} WHERE {columnName} = @value AND {className}Id != @id";
            var parameters = new DynamicParameters();
            //Thêm value chính là giá trị muốn tìm trong cột
            parameters.Add("@value", typeof(T).GetProperty(columnName).GetValue(entity));
            //Thêm id là khóa chính của thực thể
            parameters.Add("@id", typeof(T).GetProperty($"{className}Id").GetValue(entity));

            var result = Connection.QueryFirstOrDefault(sql: sqlCheck, param: parameters);

            if (result != null)
            {
                return false;
            }
            return true;
        }

        public T? FindByKeyword<T>(string? keyword, string columnName)
        {
            var className = typeof(T).Name;
            //Tạo câu lệnh SQL
            var sqlCommand = $"SELECT * FROM {className} WHERE {columnName} LIKE @keyword";
            var parameters = new DynamicParameters();
            //Thêm keyword chính là giá trị muốn tìm trong cột
            parameters.Add("@keyword", keyword);

            var result = Connection.QueryFirstOrDefault<T>(sql: sqlCommand, param: parameters);

            return result;
        }

        public string? FindLargestValueEndsWithNumberInColumn<T>(string columnName)
        {
            var className = typeof(T).Name;
            //Tạo câu lệnh SQL
            var sqlCommand = $"SELECT {columnName} FROM {className} WHERE REGEXP_LIKE({columnName}, '[0-9]$') ORDER BY CAST(SUBSTRING({columnName}, LENGTH({columnName}) - LENGTH(REGEXP_SUBSTR({columnName}, '[0-9]+$')) + 1) AS UNSIGNED) DESC LIMIT 1;";

            var result = Connection.QueryFirstOrDefault<string>(sql: sqlCommand);

            return result;
        }

        public IEnumerable<T> GetAllOrderByColumn<T>(string columnName, bool DESC = false)
        {
            var className = typeof(T).Name;

            string sort = "ASC";
            if(DESC == true)
            {
                sort = "DESC";
            }
            var sqlCommand = $"SELECT * FROM {className} ORDER BY {columnName} {sort}";
            var result = Connection.Query<T>(sql: sqlCommand);
            return result;
        }

        public IEnumerable<T> GetPaging<T>(int pageSize, int pageIndex, string orderByColumn, bool DESC = false)
        {
            var className = typeof(T).Name;

            string sort = "ASC";
            if (DESC == true)
            {
                sort = "DESC";
            }
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            var sqlCommand = $"SELECT * FROM {className} ORDER BY {orderByColumn} {sort} LIMIT {pageSize} OFFSET {pageSize*(pageIndex-1)}";
            var result = Connection.Query<T>(sql: sqlCommand);
            return result;
        }
        #endregion
}
