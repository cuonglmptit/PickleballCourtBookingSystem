namespace PickleballCourtBookingSystem.Api.DTOs;

public class AddBookingRequest
{
    public Guid UserId { get; set; }
    public List<Guid> CourtTimeSlotsIds { get; set; } = [];
    public Guid CourtId { get; set; }
}
