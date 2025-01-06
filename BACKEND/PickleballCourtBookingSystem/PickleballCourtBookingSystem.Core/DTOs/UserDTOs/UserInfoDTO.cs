namespace PickleballCourtBookingSystem.Core.DTOs.UserDTOs;

public class UserInfoDto
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; }
}