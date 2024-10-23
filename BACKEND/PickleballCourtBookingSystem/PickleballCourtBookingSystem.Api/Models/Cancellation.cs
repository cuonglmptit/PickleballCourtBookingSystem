namespace PickleballCourtBookingSystem.Api.Models

{
    public class Cancellation
    {
        public Guid? Id { get; set; }
        public DateTime TimeCancel { get; set; }
        public string BookingId { get; set; }
    }
}