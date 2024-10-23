namespace PickleballCourtBookingSystem.Api.Models

{
    public class Booking
    {
        public Guid? Id { get; set; }
        public DateTime TimeBooking { get; set; }
        public float Amount { get; set; }
        public int PaymentStatus { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CourtId { get; set; }
    }
}
