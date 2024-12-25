using PickleballCourtBookingSystem.Core.CustomAttribute;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Dapper;
using System.Text;


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
        Connection = new MySqlConnection(config.GetConnectionString("MySQLRemote"));
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

    public T? GetById<T>(Guid entityId)
    {
        var className = typeof(T).Name;

        //Tạo câu lệnh SQL
        var sqlCommand = $"SELECT * FROM {className} WHERE Id = @entityId";

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
        var propListName = string.Empty;
        var propListValue = string.Empty;

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

    // insert nhieu phan tu, neu loi thi them paramindex (Co loi that, phai them index vao)
    public int InsertMany<T>(List<T> entities)
    {
        var className = typeof(T).Name;
        var propListName = string.Empty;
        var valuesList = new List<string>();
        var parameters = new DynamicParameters();
        var properties = typeof(T).GetProperties();

        // Xây dựng danh sách các cột (property names)
        foreach (var property in properties)
        {
            var propName = property.Name;
            if (!Attribute.IsDefined(property, typeof(NotInQuery)))
            {
                propListName += $"{propName},";
            }
        }
        propListName = propListName.TrimEnd(',');

        // Xử lý từng entity
        for (var i = 0; i < entities.Count; i++)
        {
            var entity = entities[i];
            var propListValueForOne = string.Empty;

            foreach (var property in properties)
            {
                var propName = property.Name;
                if (!Attribute.IsDefined(property, typeof(NotInQuery)))
                {
                    var paramKey = $"@{propName}_{i}";
                    propListValueForOne += $"{paramKey},";
                    parameters.Add(paramKey, property.GetValue(entity));
                }
            }

            propListValueForOne = propListValueForOne.TrimEnd(',');
            valuesList.Add($"({propListValueForOne})");
        }

        // Ghép nối tất cả các giá trị
        var allValues = string.Join(",", valuesList);
        var sqlCommand = $"INSERT INTO {className}({propListName}) VALUES {allValues}";

        // Thực thi lệnh SQL
        var res = Connection.Execute(sql: sqlCommand, param: parameters);
        return res;
    }


    public int Update<T>(T entity, Guid entityId)
    {
        var className = typeof(T).Name;

        // Khai báo biến các prop/column và value của thực thể
        var setClause = string.Empty;

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
            if (!Attribute.IsDefined(property, typeof(NotInQuery)) && !Attribute.IsDefined(property, typeof(PrimaryKey)) && !Attribute.IsDefined(property, typeof(ForeignKey)))
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
        var sqlCommand = $"UPDATE {className} SET {setClause} WHERE Id = @entityId";

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
        var sqlCommand = $"DELETE FROM {className} WHERE Id = @entityId";

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
        var sqlCommand = $"DELETE FROM {className} WHERE Id IN @ids";

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
        var sqlCheck = $"SELECT * FROM {className} WHERE {columnName} = @value AND Id != @id";
        var parameters = new DynamicParameters();
        //Thêm value chính là giá trị muốn tìm trong cột
        parameters.Add("@value", typeof(T).GetProperty(columnName).GetValue(entity));
        //Thêm id là khóa chính của thực thể
        parameters.Add("@id", typeof(T).GetProperty("Id").GetValue(entity));

        var result = Connection.QueryFirstOrDefault(sql: sqlCheck, param: parameters);

        if (result != null)
        {
            return false;
        }
        return true;
    }


    public List<string> HasDuplicateValuesInOtherRecords<T>(T entity, List<string> columns)
    {
        var className = typeof(T).Name;
        var parameters = new DynamicParameters();

        // Thêm giá trị của các cột vào parameters
        foreach (var column in columns)
        {
            parameters.Add($"@{column}", typeof(T).GetProperty(column)?.GetValue(entity));
        }

        // Thêm Id vào điều kiện
        parameters.Add("@Id", typeof(T).GetProperty("Id")?.GetValue(entity));

        // Tạo câu lệnh SQL động
        var sqlCheck = $@"
                    {string.Join(" UNION ", columns.Select(column => $@"
                        SELECT '{column}' AS DuplicatedColumn
                        FROM {className}
                        WHERE {column} = @{column}
                          AND Id != @Id
                    "))};";

        // In ra câu lệnh SQL để kiểm tra
        //Console.WriteLine(sqlCheck);

        // Thực hiện truy vấn và trả về danh sách các cột bị trùng
        var duplicatedColumns = Connection.Query<string>(sqlCheck, parameters).ToList();
        return duplicatedColumns;
    }


    public IEnumerable<T> SearchByKeyword<T>(string? keyword, string columnName)
    {
        var className = typeof(T).Name;
        var newkeyword = "%" + keyword + "%";
        //Tạo câu lệnh SQL
        var sqlCommand = $"SELECT * FROM {className} WHERE {columnName} LIKE @keyword";
        var parameters = new DynamicParameters();
        //Thêm keyword chính là giá trị muốn tìm trong cột
        parameters.Add("@keyword", newkeyword);
        var result = Connection.Query<T>(sql: sqlCommand, param: parameters);

        return result;
    }

    public IEnumerable<T> SearchByKeywordMultipleColumns<T>(string? keyword, List<string> listColumnName)
    {
        var className = typeof(T).Name;
        var newKeyword = "%" + keyword + "%";
        var conditions = new List<string>();
        for (int i = 0; i < listColumnName.Count; i++)
        {
            string columnName = listColumnName[i];
            conditions.Add($"{columnName} LIKE @keyword");
        }
        var whereClause = string.Join(" OR ", conditions);
        var sqlCommand = $"SELECT * FROM {className} WHERE {whereClause}";
        var parameters = new DynamicParameters();
        for (int i = 0; i < listColumnName.Count; i++)
        {
            parameters.Add("@keyword", newKeyword);
        }
        var result = Connection.Query<T>(sql: sqlCommand, param: parameters);
        return result;
    }

    public IEnumerable<T> FindByColumnValue<T>(object? value, string columnName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null");
        }
        // if (value is not (int or string))
        // {
        //     throw new ArgumentException("Value must be of type int or string", nameof(value));
        // }

        // var newValue = value.ToString();
        var className = typeof(T).Name;
        //Tạo câu lệnh SQL
        var sqlCommand = $"SELECT * FROM {className} WHERE {columnName} = @keyword";
        var parameters = new DynamicParameters();
        //Thêm keyword chính là giá trị muốn tìm trong cột
        parameters.Add("@keyword", value);

        var result = Connection.Query<T>(sql: sqlCommand, param: parameters);

        return result;
    }

    public T? FindFirstByColumnvalue<T>(string? value, string columnName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value), "Value cannot be null");
        }
        // if (value is not (int or string))
        // {
        //     throw new ArgumentException("Value must be of type int or string", nameof(value));
        // }
        var className = typeof(T).Name;
        //Tạo câu lệnh SQL
        var sqlCommand = $"SELECT * FROM {className} WHERE {columnName} = @keyword";
        Console.WriteLine("hhh" + sqlCommand);
        var parameters = new DynamicParameters();
        //Thêm keyword chính là giá trị muốn tìm trong cột
        parameters.Add("@keyword", value);
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
        if (DESC == true)
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
        var sqlCommand = $"SELECT * FROM {className} ORDER BY {orderByColumn} {sort} LIMIT {pageSize} OFFSET {pageSize * (pageIndex - 1)}";
        var result = Connection.Query<T>(sql: sqlCommand);
        return result;
    }

    public int UpdateSpecifiedColumns<T>(T entity, Guid entityId, List<string> columns)
    {
        var className = typeof(T).Name;

        // Khai báo biến các prop/column và value của thực thể
        var setClause = string.Empty;

        // Tạo dynamic parameters
        var parameters = new DynamicParameters();

        // Lấy ra các property của entity
        var properties = entity.GetType().GetProperties();

        // Duyệt qua các prop và build câu lệnh update
        foreach (var property in properties)
        {
            // Lấy ra tên của property
            var propName = property.Name;

            // Nếu tên property có trong danh sách columns thì tiếp tục
            if (columns.Contains(propName))
            {
                // Lấy ra value của property
                var propValue = property.GetValue(entity);
                // Nếu không đánh dấu NotInQuery thì mới thêm vào, Muốn đổi thành id khác cũng không được
                if (!Attribute.IsDefined(property, typeof(NotInQuery)) && !Attribute.IsDefined(property, typeof(PrimaryKey)) && !Attribute.IsDefined(property, typeof(ForeignKey)))
                {

                    // Xây dựng phần SET của câu lệnh UPDATE
                    setClause += $"{propName} = @{propName}, ";

                    // Thêm vào dynamic parameters
                    parameters.Add($"@{propName}", propValue);
                }
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
        var sqlCommand = $"UPDATE {className} SET {setClause} WHERE Id = @entityId";

        // Khai báo biến số bản ghi ảnh hưởng
        var res = 0;

        // Thực thi câu lệnh
        res += Connection.Execute(sql: sqlCommand, param: parameters);

        // Trả về kết quả
        return res;
    }

    public IEnumerable<T> GetByMultipleConditions<T>(Dictionary<string, object> conditions)
    {
        // Nếu không có điều kiện, trả về danh sách trống
        if (conditions == null || conditions.Count == 0)
        {
            return Enumerable.Empty<T>();
        }

        // Xây dựng câu lệnh SQL WHERE
        var whereClause = new StringBuilder("WHERE ");
        var parameters = new DynamicParameters();

        foreach (var condition in conditions)
        {
            whereClause.Append($"{condition.Key} = @{condition.Key} AND ");
            parameters.Add($"@{condition.Key}", condition.Value);
        }

        // Loại bỏ " AND " cuối cùng
        whereClause.Length -= 5;

        // Tạo câu truy vấn SQL
        var sql = $"SELECT * FROM {typeof(T).Name} {whereClause}";

        Console.WriteLine(sql);
        // Thực thi truy vấn và trả về kết quả
        return Connection.Query<T>(sql, parameters);
    }

    #endregion
}
