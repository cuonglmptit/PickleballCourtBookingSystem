namespace PickleballCourtBookingSystem.Api.Models

{
    public class Court
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public Guid AddressId { get; set; }
        public Guid CourseOwnerId { get; set; }
    }
}
