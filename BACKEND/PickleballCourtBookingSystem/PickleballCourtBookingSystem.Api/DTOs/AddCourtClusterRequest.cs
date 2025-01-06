using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Api.DTOs;

public record AddCourtClusterRequest(
    string Name,
    string? Description,
    TimeSpan OpeningTime,
    TimeSpan ClosingTime,
    string City,
    string District,
    string Ward,
    string Street,
    int NumberOfCourts,
    IFormFile? Image
    );