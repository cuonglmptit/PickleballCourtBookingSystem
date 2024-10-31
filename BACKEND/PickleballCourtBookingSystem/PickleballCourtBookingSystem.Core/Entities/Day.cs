namespace PickleballCourtBookingSystem.Api.Models
{
    public class Day
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public int TotalSlotAvailable { get; set; }
    }
}
