namespace PickleballCourtBookingSystem.Api.Models

{
    public class Court
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string AddressId { get; set; }
        public string CourseOwnerId { get; set; }
    }
}
