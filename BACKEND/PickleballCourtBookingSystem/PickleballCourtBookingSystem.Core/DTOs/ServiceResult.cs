namespace PickleballCourtBookingSystem.Core.DTOs

{
    /// <summary>
    /// Class để trả về các kết quả của thao tác
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Kết quả của service true - Thực hiện thành công, false - Thất bại
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Thông báo cho dev
        /// </summary>
        public string? DevMsg { get; set; }
        /// <summary>
        /// Thông báo cho user
        /// </summary>
        public string? UserMsg { get; set; }
        /// <summary>
        /// Data trả về
        /// </summary>
        public object? Data { get; set; }
        /// <summary>
        /// Status code
        /// </summary>
        public int StatusCode { get; set; }

        public override string ToString()
        {
            return $"Success: {Success}, " +
                   $"DevMsg: {(string.IsNullOrEmpty(DevMsg) ? "N/A" : DevMsg)}, " +
                   $"UserMsg: {(string.IsNullOrEmpty(UserMsg) ? "N/A" : UserMsg)}, " +
                   $"StatusCode: {StatusCode}, " +
                   $"Data: {(Data != null ? Data.ToString() : "N/A")}";
        }
    }
}