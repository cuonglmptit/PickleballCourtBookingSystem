using PickleballCourtBookingSystem.Core.Entities;
namespace PickleballCourtBookingSystem.Core.DTOs;

public class CustomBookingResponse
{
    public Booking? Booking { get; set; }
    public string? CourtClusterName { get; set; }
    public int? CourtNumber { get; set; }
    public Address? Address { get; set; }
    public List<CourtTimeSlot>? CourtTimeSlots { get; set; }
    public string? CourtOwnerPhoneNumber { get; set; }
    
    public string? CustomerPhoneNumber { get; set; }
    
    public DateTime LastUpdatedTime { get; set; }
}