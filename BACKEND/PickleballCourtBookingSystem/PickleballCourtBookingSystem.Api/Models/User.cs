namespace PickleballCourtBookingSystem.Api.Models
{
    public class User
    {
        public Guid? Id { get; set; }
        public int Code { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int IsActive { get; set; }
        public string AddressId { get; set; }
        public string FullNameId { get; set; }
    }
}
