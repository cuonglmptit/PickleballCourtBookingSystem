namespace PickleballCourtBookingSystem.Api.Models

{
    public class Booking
    {
        public Guid? Id { get; set; }
        public DateTime TimeBooking { get; set; }
        public float Amount { get; set; }
        public int PaymentStatus { get; set; }
        public string CustomerId { get; set; }
        public string CourtId { get; set; }
    }
}
