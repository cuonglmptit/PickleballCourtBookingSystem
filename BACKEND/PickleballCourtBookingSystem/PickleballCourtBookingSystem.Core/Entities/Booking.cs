using PickleballCourtBookingSystem.Core.PEnum;

namespace PickleballCourtBookingSystem.Core.Entities

{
    public class Booking
    {
        public Guid? Id { get; set; }
        public DateTime? TimeBooking { get; set; }
        public double? Amount { get; set; }
        public BookingStatusEnum? Status { get; set; }
        public PaymentStatusEnum? PaymentStatus { get; set; }
        public Guid? CourtId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? CourtClusterId { get; set; }
        public Guid? CourtOwnerId { get; set; }
    }
}