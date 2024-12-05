namespace PickleballCourtBookingSystem.Api.DTOs;

public class TimeRangeRequest
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}