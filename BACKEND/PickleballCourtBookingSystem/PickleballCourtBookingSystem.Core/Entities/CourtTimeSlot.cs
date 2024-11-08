namespace PickleballCourtBookingSystem.Api.Models

{
    public class CourtTimeSlot
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int IsAvailable { get; set; }
        public double Price { get; set; }
        public Guid CourtId { get; set; }
    }
}
