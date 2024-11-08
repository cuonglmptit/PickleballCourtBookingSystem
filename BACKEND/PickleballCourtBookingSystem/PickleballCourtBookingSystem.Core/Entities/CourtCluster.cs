namespace PickleballCourtBookingSystem.Api.Models

{
    public class CourtCluster
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AddressId { get; set; }
        public Guid CourtOwnerId { get; set; } 
    }
}
