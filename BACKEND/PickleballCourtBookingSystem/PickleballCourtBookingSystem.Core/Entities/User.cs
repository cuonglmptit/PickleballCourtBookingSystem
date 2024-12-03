namespace PickleballCourtBookingSystem.Core.Entities
{
    public class User
    {
        public Guid? Id { get; set; }
        public int? Code { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? IsActive { get; set; }
        public string? AvatarUrl { get; set; }
        public Guid? AddressId { get; set; }
        public int? RoleId { get; set; }
    }
}
