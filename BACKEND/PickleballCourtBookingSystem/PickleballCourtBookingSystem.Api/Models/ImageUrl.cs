namespace PickleballCourtBookingSystem.Api.Models

{
    public class ImageUrl
    {
        public Guid? Id { get; set; }
        public string Url { get; set; }
        public string CourtId { get; set; }
    }
}