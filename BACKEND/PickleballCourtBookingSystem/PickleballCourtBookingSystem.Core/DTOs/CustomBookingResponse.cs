using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Api.DTOs;

public class CustomBookingResponse
{
    public Booking? Booking { get; set; }
    public string? CourtClusterName { get; set; }
    public int? CourtNumber { get; set; }
    public Address? Address { get; set; }
    public List<CourtTimeSlot>? CourtTimeSlots { get; set; }
    public string? CourtOwnerPhoneNumber { get; set; }
    
    public DateTime LastUpdatedTime { get; set; }
}