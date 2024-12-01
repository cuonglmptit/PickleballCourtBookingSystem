using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class BookingService : BaseService<Booking>, IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoleService _roleService;
    private readonly ICourtTimeSlotService _courtTimeSlotService;
    private readonly ICourtService _courtService;
    private readonly ICourtTimeBookingService _courtTimeBookingService;
    private readonly ICustomerService _customerService;
    private readonly ITimeService _timeService;
    public BookingService(IBookingRepository repository,
        IRoleService roleService,
        ICourtTimeSlotService courtTimeSlotService, ICourtTimeBookingService courtTimeBookingService, ICourtService courtService, ICustomerService customerService, IBookingRepository bookingRepository, ITimeService timeService) : base(repository)
    {
        _roleService = roleService;
        _courtTimeSlotService = courtTimeSlotService;
        _courtTimeBookingService = courtTimeBookingService;
        _courtService = courtService;
        _customerService = customerService;
        _bookingRepository = bookingRepository;
        _timeService = timeService;
    }

    public ServiceResult AddBooking(User user, List<CourtTimeSlot> courtTimeSlots, Court court)
    {
        if (user.Id == Guid.Empty || user.Id == null || courtTimeSlots.Count == 0 || court.Id == Guid.Empty ||
            court.Id == null)
        {
            return CreateServiceResult(false, StatusCode: 400, UserMsg: "Request error", DevMsg: "Request error");
        }
        var roleServiceResult = _roleService.GetUserRoleByUser(user);
        if (!roleServiceResult.Success)
        {
            return roleServiceResult;
        }
        var courtServiceResult = _courtService.GetByIdService(court.Id.Value);
        if (!courtServiceResult.Success)
        {
            return courtServiceResult;
        }
        var courtCheck = (Court) courtServiceResult.Data!;
        if (courtCheck.CourtClusterId != court.CourtClusterId)
        {
            return CreateServiceResult(false, StatusCode: 400, UserMsg: "Request error", DevMsg: "Request error");
        }
        var Amount = 0.00;
        foreach (var courtTimeSlot in courtTimeSlots)
        {
            if (courtTimeSlot.CourtId != court.Id)
            {
                return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Court Time Slot khong phai cua Court", DevMsg: "Court Time Slot khong phai cua Court");
            }
            var courtTimeSlotServiceResult = _courtTimeSlotService.CheckTimeSlotAvailableAndCheckPrice(courtTimeSlot);
            if (!courtTimeSlotServiceResult.Success)
            {
                return courtTimeSlotServiceResult;
            }
            if (courtTimeSlot.Price.HasValue)
            {
                Amount += courtTimeSlot.Price.Value;
            }
        }

        var customerServiceResult = _customerService.GetCustomerByUserIdService(user.Id);
        if (!customerServiceResult.Success)
        {
            return customerServiceResult;
        }

        var customer = (Customer)customerServiceResult.Data!;
        var customerId = customer.Id;
        var booking = new Booking();
        booking.Id = Guid.NewGuid();
        booking.CourtId = court.Id;
        booking.CustomerId = customerId;
        booking.CourtClusterId = court.CourtClusterId;
        booking.TimeBooking = _timeService.GetCurrentTime();
        booking.Status = 0;
        booking.PaymentStatus = 0;
        booking.Amount = Amount;
        var listCourtTimeBooking = new List<CourtTimeBooking>();
        var resultAddBooking = _bookingRepository.Insert(booking);
        if (resultAddBooking == 0)
        {
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Failed to add booking", DevMsg: "Failed to add booking");
        }
        foreach (var courtTimeSlot in courtTimeSlots)
        {
            var courtTimeBooking = new CourtTimeBooking();
            courtTimeBooking.Id = Guid.NewGuid();
            courtTimeBooking.BookingId = booking.Id;
            courtTimeBooking.CourtTimeSlotId = courtTimeSlot.Id;
            listCourtTimeBooking.Add(courtTimeBooking);
        }
        var resultAddCourtTimeBooking = _courtTimeBookingService.InsertManyService(listCourtTimeBooking);
        if (!resultAddCourtTimeBooking.Success)
        {
            var resultRemoveBooking = _bookingRepository.Delete(booking.Id.Value);
            return resultAddCourtTimeBooking;
        }
        return CreateServiceResult(Success: true, StatusCode: 201, UserMsg: "Tao booking thanh cong", DevMsg: "Success to add booking");
    }

    public ServiceResult CancelBooking(Booking booking)
    {
        return CreateServiceResult(Success: false, StatusCode:400);
    }
}