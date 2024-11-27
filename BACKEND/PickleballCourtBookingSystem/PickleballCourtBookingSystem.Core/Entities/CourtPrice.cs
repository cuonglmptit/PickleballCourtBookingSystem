namespace PickleballCourtBookingSystem.Api.Models


{
    public class CourtPrice
    {
        public Guid? Id { get; set; }
        public TimeSpan? Time { get; set; }
        public Double? Price { get; set; }
        public Guid? CourtClusterId { get; set; }
    }
}