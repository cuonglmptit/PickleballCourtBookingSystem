namespace PickleballCourtBookingSystem.Api.DTOs
{
    public class GetTimeSlotDTO
    {
        public Guid CourtId { get; set; }
        public DateTime Date { get; set; }
    }
}
