namespace PickleballCourtBookingSystem.Core.Entities

{
    public class Cancellation
    {
        public Guid? Id { get; set; }
        public DateTime? TimeCancel { get; set; }
        public string? Reason { get; set; }
        public Guid? BookingId { get; set; }
    }
}