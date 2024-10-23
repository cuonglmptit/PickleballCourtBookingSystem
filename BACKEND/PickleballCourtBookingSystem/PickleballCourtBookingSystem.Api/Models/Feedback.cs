namespace PickleballCourtBookingSystem.Api.Models

{
    public class Feedback
    {
        public Guid? Id { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public string CustomerId { get; set; }
        public string CourtId { get; set; }
        public string BookingId { get; set; }
    }
}