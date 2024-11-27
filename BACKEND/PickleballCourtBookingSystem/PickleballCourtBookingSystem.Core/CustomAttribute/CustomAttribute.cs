namespace PickleballCourtBookingSystem.Core.CustomAttribute

{
    /// <summary>
    /// Đánh dấu là trường không trong query
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotInQuery : Attribute
    {
    }

    /// <summary>
    /// Đánh dấu là trường không được trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {
    }

    /// <summary>
    /// Đánh dấu là trường khóa chính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {
    }
    
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKey : Attribute
    {
        
    }
}