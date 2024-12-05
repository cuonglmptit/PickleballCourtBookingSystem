namespace PickleballCourtBookingSystem.Api.DTOs;

public class AutoAddCourtTimeSlotRequest
{
    public Guid CourtClusterId { get; set; }
    public List<DateTime> Dates { get; set; } = [];
}