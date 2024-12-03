using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
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

    public ServiceResult AddBooking(Guid userId, List<Guid> courtTimeSlotIds, Guid courtId)
    {
        try
        {
            if (userId == Guid.Empty || courtTimeSlotIds.Count == 0 || courtId == Guid.Empty ||
                courtTimeSlotIds.Contains(Guid.Empty))
            {
                return CreateServiceResult(false, StatusCode: 400, UserMsg: "Request error", DevMsg: "Request error");
            }

            var roleServiceResult = _roleService.GetUserRoleByUserId(userId);
            if (!roleServiceResult.Success)
            {
                return roleServiceResult;
            }

            var courtServiceResult = _courtService.GetByIdService(courtId);
            if (!courtServiceResult.Success)
            {
                return courtServiceResult;
            }

            var court = (Court)courtServiceResult.Data!;
            var amount = 0.00;
            foreach (var courtTimeSlotId in courtTimeSlotIds)
            {
                var courtTimeSlot = (CourtTimeSlot)_courtTimeSlotService.GetByIdService(courtTimeSlotId).Data!;
                if (courtTimeSlot.CourtId == court.Id)
                {
                    return CreateServiceResult(false, StatusCode: 400, UserMsg: "Request error",
                        DevMsg: "Request error");
                }

                if (courtTimeSlot.IsAvailable == 0)
                {
                    return CreateServiceResult(false, StatusCode: 400, UserMsg: "Lich san da bi dang ky",
                        DevMsg: "Lich san da bi dang ky");
                }

                if (courtTimeSlot.Date != null && courtTimeSlot.Time != null &&
                    courtTimeSlot.Date.Value + courtTimeSlot.Time.Value < _timeService.GetCurrentTime())
                {
                    return CreateServiceResult(false, StatusCode: 400,
                        UserMsg: "Ban dang ky san vao thoi gian trong qua khu",
                        DevMsg: "Thoi gian dang ky san o qua khu");
                }

                if (courtTimeSlot.Price.HasValue)
                {
                    amount += courtTimeSlot.Price.Value;
                }
            }

            var customerServiceResult = _customerService.GetCustomerByUserIdService(userId);
            if (!customerServiceResult.Success)
            {
                return customerServiceResult;
            }

            var customer = (Customer)customerServiceResult.Data!;
            var customerId = customer.Id;
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                CourtId = court.Id,
                CustomerId = customerId,
                CourtClusterId = court.CourtClusterId,
                TimeBooking = _timeService.GetCurrentTime(),
                Status = 0,
                PaymentStatus = 0,
                Amount = amount
            };
            var listCourtTimeBooking = new List<CourtTimeBooking>();
            var resultAddBooking = _bookingRepository.Insert(booking);
            if (resultAddBooking == 0)
            {
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Failed to add booking",
                    DevMsg: "Failed to add booking");
            }

            foreach (var courtTimeSlotId in courtTimeSlotIds)
            {
                var courtTimeBooking = new CourtTimeBooking
                {
                    Id = Guid.NewGuid(),
                    BookingId = booking.Id,
                    CourtTimeSlotId = courtTimeSlotId
                };
                listCourtTimeBooking.Add(courtTimeBooking);
            }

            var resultAddCourtTimeBooking = _courtTimeBookingService.InsertManyService(listCourtTimeBooking);
            if (resultAddCourtTimeBooking.Success)
            {
                return CreateServiceResult(Success: true, StatusCode: 201, UserMsg: "Tao booking thanh cong",
                    DevMsg: "Success to add booking");

            }

            var resultRemoveBooking = _bookingRepository.Delete(booking.Id.Value);
            return resultAddCourtTimeBooking;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Failed to add booking", DevMsg: e.Message);
        }
    }

    public ServiceResult CancelBooking(Booking booking)
    {
        return CreateServiceResult(Success: false, StatusCode:400);
    }
}