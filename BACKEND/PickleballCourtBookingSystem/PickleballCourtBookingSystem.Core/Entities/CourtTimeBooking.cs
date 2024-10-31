namespace PickleballCourtBookingSystem.Api.Models
{
    public class CourtTimeBooking
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid CourtTimeId { get; set; }
    }
}


