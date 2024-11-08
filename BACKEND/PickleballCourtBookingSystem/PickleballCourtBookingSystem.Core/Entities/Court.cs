namespace PickleballCourtBookingSystem.Api.Models

{
    public class Court
    {
        public Guid Id { get; set; }
        public int CourtNumber { get; set; }
        public string Description { get; set; }
        public Guid CourtClusterId { get; set; }
        public Boolean IsActive { get; set; }
    }
}
