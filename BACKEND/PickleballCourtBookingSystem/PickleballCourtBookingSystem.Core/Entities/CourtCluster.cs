namespace PickleballCourtBookingSystem.Core.Entities

{
    public class CourtCluster
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public TimeSpan? OpeningTime { get; set; }
        public TimeSpan? ClosingTime { get; set; }
        public Guid? AddressId { get; set; }
        public Guid? CourtOwnerId { get; set; } 
        public int? Status { get; set; }
    }
}
