namespace PickleballCourtBookingSystem.Core.Entities

{
    public class Court
    {
        public Guid? Id { get; set; }
        public int? CourtNumber { get; set; }
        public string? Description { get; set; }
        public Guid? CourtClusterId { get; set; }
    }
}
