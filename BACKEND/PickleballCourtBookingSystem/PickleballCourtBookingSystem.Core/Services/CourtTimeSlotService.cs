using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtTimeSlotService : BaseService<CourtTimeSlot>, ICourtTimeSlotService
{
    private readonly ICourtTimeSlotRepository _courtTimeSlotRepository;
    public CourtTimeSlotService(ICourtTimeSlotRepository repository, ICourtTimeSlotRepository courtTimeSlotRepository) : base(repository)
    {
        _courtTimeSlotRepository = courtTimeSlotRepository;
    }

    public ServiceResult GetAvailableCourtTimeSlotsByCourtId(Guid? courtId)
    {
        try
        {
            var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
            var date = timeNow.Date;
            var time = timeNow.TimeOfDay;
            var result = _courtTimeSlotRepository.FindAvailableCourtTimeSlotsByCourtId(courtId, date, time);
            if (result.ToList().Count == 0)
            {
                return CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Khong co du lieu trong khoang thoi gian da chon", DevMsg: "KHong co du lieu trong khoang thoi gian da chon");
            }
            return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Khong lay duoc du lieu cua court time slot", DevMsg: "Get Court Time Slot Error");
        }
    }

    public ServiceResult GetAvailableCourtTimeSlotsByCourtIdAndDate(Guid courtId, DateTime date)
    {
        try
        {
            var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
            var dateCheck = timeNow.Date;
            var time = TimeSpan.Zero;
            if (dateCheck.Date == date.Date)
            {
                time = timeNow.TimeOfDay;
            }
            var result = _courtTimeSlotRepository.FindAvailableCourtTimeSlotByCourtIdAndDate(courtId, date, time);
            if (result.ToList().Count == 0)
            {
                return CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Khong co khung gio co san cho san trong ngay da chon", DevMsg: "Khong co khung gio co san cho san trong ngay da chon");
            }
            return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Khong lay duoc du lieu cua court time slot", DevMsg: "Get Court Time Slot Error");
        }
    }

    public ServiceResult GetAvailableCourtTimeSlotForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
            if (date.Date < timeNow.Date || (timeNow.Date == date && (startTime < timeNow.TimeOfDay || endTime < timeNow.TimeOfDay)) || (startTime > endTime))
            {
                return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngay gio truyen vao khong hop le (thoi diem trong qua khu hoac gio bat dau nho hon gio ket thuc)", DevMsg: "Ngay gio truyen vao khong hop le");
            }
            var time = timeNow.TimeOfDay;
            var result = _courtTimeSlotRepository.FindAvailableCourtTimeSlotsForTimeRange(date, startTime, endTime);
            if (result.ToList().Count == 0)
            {
                return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Khong co du lieu trong khoang thoi gian da chon", DevMsg: "KHong co du lieu trong khoang thoi gian da chon");
            }
            return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Lay du lieu court time slot bi loi", DevMsg: "Lay du lieu bi loi");
        }
    }

    public ServiceResult AddCourtTimeSlotWithDefaultPrice(List<Guid> courtIds, IEnumerable<DateTime> dates, IEnumerable<CourtPrice> prices)
    {
        try
        {

            var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
            var courtTimeSlots = new List<CourtTimeSlot>();
            foreach (var courtId in courtIds)
            {
                var courtTimeSlotsCheck = _courtTimeSlotRepository.FindCourtTimeSlotsByCourtId(courtId, timeNow.Date, timeNow.TimeOfDay);

                foreach (var date in dates)
                {
                    if (date.Date < timeNow.Date)
                    {
                        return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngay truyen vao khong hop le (thoi diem trong qua khu)", DevMsg: "Ngay truyen vao khong hop le");
                    }
                    foreach (var courtPrice in prices)
                    {
                        if (!courtPrice.Time.HasValue) continue;
                        var check = true;
                        foreach (var courtTimeSlotCheck in courtTimeSlotsCheck)
                        {
                            if ((courtTimeSlotCheck.Date == date.Date && courtTimeSlotCheck.Time == courtPrice.Time &&
                                courtTimeSlotCheck.CourtId == courtId) || (date.Date == courtTimeSlotCheck.Date && courtPrice.Time < timeNow.TimeOfDay))
                            {
                                check = false;
                            }
                        }
                        if (check)
                        {
                            courtTimeSlots.Add(new CourtTimeSlot
                            {
                                Id = Guid.NewGuid(),
                                Date = date,
                                Time = courtPrice.Time,
                                IsAvailable = 1,
                                Price = courtPrice.Price,
                                CourtId = courtId
                            });
                        }
                    }
                }
            }

            if (courtTimeSlots.Count == 0)
            {
                return CreateServiceResult(Success: true, StatusCode: 201, UserMsg: "Khong them du lieu gi", DevMsg: "Khong them du lieu gi");
            }
            var res = baseRepository.InsertMany(courtTimeSlots);
            return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 201 : 400);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Them du lieu court time slot bi loi", DevMsg: "Them du lieu bi loi");
        }
    }

    public ServiceResult FindCourtTimeSlotsByCourtId(Guid courtId, DateTime date, TimeSpan? time = null)
    {
        try
        {
            //Nếu mà ngày hôm nay thì time là hiện tại
            if (time == null)
            {
                var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
                if (date.Date == timeNow.Date)
                {
                    time = timeNow.TimeOfDay;
                }
            }
            //Nếu mà ngày lớn hơn hôm nay thì thời gian bắt đây là 00:00:00
            if (time == null)
            {
                time = TimeSpan.Zero;
            }

            //In ra ngày và thời gian
            Console.WriteLine(date);
            Console.WriteLine(time);

            var result = _courtTimeSlotRepository.FindCourtTimeSlotsByCourtId(courtId, date, time.Value);
            if (result.ToList().Count == 0)
            {
                return CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Khong co du lieu trong khoang thoi gian da chon", DevMsg: "KHong co du lieu trong khoang thoi gian da chon");
            }
            return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
        }
        catch (Exception e)
        {
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Lay du lieu court time slot bi loi", DevMsg: e.Message);
        }
    }
}