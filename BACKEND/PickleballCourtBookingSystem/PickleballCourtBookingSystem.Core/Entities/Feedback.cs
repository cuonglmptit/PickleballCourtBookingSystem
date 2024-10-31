namespace PickleballCourtBookingSystem.Api.Models

{
    public class Feedback
    {
        public Guid Id { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CourtId { get; set; }
        public Guid BookingId { get; set; }
    }
}