namespace PickleballCourtBookingSystem.Api.DTOs;

public record AddBookingRequest
(
    List<Guid> CourtTimeSlotsIds,
    Guid CourtId
);
