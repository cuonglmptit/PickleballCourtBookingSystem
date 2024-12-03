namespace PickleballCourtBookingSystem.Core.Entities

{
    public class Address
    {
        public Guid? Id { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Street { get; set; }
    }
}
