namespace PickleballCourtBookingSystem.Core.Entities
{
    public class CourtTimeBooking
    {
        public Guid? Id { get; set; }
        public Guid? BookingId { get; set; }
        public Guid? CourtTimeSlotId { get; set; }
    }
}


