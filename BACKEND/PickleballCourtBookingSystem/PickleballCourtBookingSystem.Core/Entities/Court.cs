namespace PickleballCourtBookingSystem.Api.Models

{
    public class Court
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid AddressId { get; set; }
        public Guid CourseOwnerId { get; set; }
    }
}
