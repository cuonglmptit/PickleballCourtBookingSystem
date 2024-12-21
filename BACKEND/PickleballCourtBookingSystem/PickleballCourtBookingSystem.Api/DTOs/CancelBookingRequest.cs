namespace PickleballCourtBookingSystem.Api.DTOs;

public record CancelBookingRequest(
    Guid BookingId,
    string? Reason
);