namespace PickleballCourtBookingSystem.Api.Models
{
    public class User
    {
        //Id người dùng
        public Guid? UserId { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
