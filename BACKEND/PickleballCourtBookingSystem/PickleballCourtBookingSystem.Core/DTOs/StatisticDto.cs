using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.DTOs;

public class StatisticDto
{
    public CourtCluster? CourtCluster { get; set; }
    public List<Booking>? Bookings { get; set; }
    public double TotalRevenue { get; set; } = 0.0;
    public int TotalBookings { get; set; } = 0;
}