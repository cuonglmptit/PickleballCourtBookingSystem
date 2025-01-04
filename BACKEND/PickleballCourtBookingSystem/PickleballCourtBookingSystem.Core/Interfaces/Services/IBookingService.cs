using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.PEnum;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services;

public interface IBookingService : IBaseService<Booking>
{
    public ServiceResult AddBooking(Guid userId, List<Guid> courtTimeSlotIds, Guid courtId);
    public ServiceResult CourtOwnerConfirmBooking(Guid userId, Guid bookingId);
    //public ServiceResult CustomerConfirmBooking(Guid userId, Guid bookingId);

    /// <summary>
    /// Hủy booking theo id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="bookingId"></param>
    /// <param name="reason"></param>
    /// <returns></returns>
    public ServiceResult CancelBooking(Guid userId, Guid bookingId, string? reason);
    /// <summary>
    /// Lấy ra các booking theo trạng thái
    /// </summary>
    /// <param name="status">Trạng thái</param>
    /// <returns>Danh sách booking</returns>
    public ServiceResult GetBookingByStatusService(Guid userId, BookingStatusEnum status);

    /// <summary>
    /// Lấy ra các booking theo trạng thái
    /// </summary>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    public ServiceResult GetCourtTimeSlotBookingIdService(Guid bookingId);

    /// <summary>
    /// Xác nhân đã nhận tiền từ khách hàng
    /// </summary>
    /// <param name="courtOwnerId"></param>
    /// <param name="bookingId"></param>
    /// <returns></returns>
    public ServiceResult CourtOwnerConfirmPaid(Guid courtOwnerId, Guid bookingId);
}