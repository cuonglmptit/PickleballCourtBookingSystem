namespace PickleballCourtBookingSystem.Api.Models

{
    public class ImageCourtURL
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public Guid CourtClusterId { get; set; }
    }
}