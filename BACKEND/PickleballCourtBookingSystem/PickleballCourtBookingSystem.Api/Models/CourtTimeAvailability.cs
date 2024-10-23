namespace PickleballCourtBookingSystem.Api.Models

{
    public class CourtTimeAvailability
    {
        public Guid? Id { get; set; }
        public TimeSpan Time { get; set; }
        public int IsAvailable { get; set; }
        public string CourtId { get; set; }
        public string DayId { get; set; }
    }
}
