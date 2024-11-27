namespace PickleballCourtBookingSystem.Api.Models

{
    public class ImageCourtUrl
    {
        public Guid? Id { get; set; }
        public string? Url { get; set; }
        public Guid? CourtClusterId { get; set; }
    }
}