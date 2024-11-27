namespace PickleballCourtBookingSystem.Api.Models

{
    public class Booking
    {
        public Guid? Id { get; set; }
        public DateTime? TimeBooking { get; set; }
        public double? Amount { get; set; }
        public int? PaymentStatus { get; set; }
        public Guid? CourtId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
