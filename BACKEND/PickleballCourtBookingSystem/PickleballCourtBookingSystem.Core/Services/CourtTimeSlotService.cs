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
            var res = _courtTimeSlotRepository.FindAvailableCourtTimeSlotsByCourtId(courtId, date, time);
            return CreateServiceResult(Success: true, StatusCode: 200, Data: res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Khong lay duoc du lieu cua court time slot", DevMsg: "Get Court Time Slot Error");
        }
    }

    public ServiceResult GetAvailableCourtTimeSlotForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
            if (timeNow.Date < date || (timeNow.Date == date && (startTime < timeNow.TimeOfDay || endTime < timeNow.TimeOfDay)) || (startTime > endTime))
            {
                return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngay gio truyen vao khong hop le (thoi diem trong qua khu hoac gio bat dau nho hon gio ket thuc)", DevMsg: "Ngay gio truyen vao khong hop le");
            }
            var time = timeNow.TimeOfDay;
            var result = _courtTimeSlotRepository.FindAvailableCourtTimeSlotsForTimeRange(date, startTime, endTime);
            return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Lay du lieu court time slot bi loi", DevMsg: "Lay du lieu bi loi");
        }
    }

    // public ServiceResult AddCourtTimeSlotWithDefaultPrice(Guid courtId, List<DateTime> dates, Dictionary<string, double> prices)
    // {
    //     try
    //     {
    //         var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
    //         var date = timeNow.Date;
    //         var time = timeNow.TimeOfDay;
    //         var courtTimeSlotsCheck = _courtTimeSlotRepository.FindCourtTimeSlotsByCourtId(courtId, date, time);
    //         var timeCheck = new HashSet<DateTime>();
    //         foreach (var courtTimeSlot in courtTimeSlotsCheck)
    //         {
    //             if (courtTimeSlot.Date.HasValue && courtTimeSlot.Time.HasValue)
    //             {
    //                 var dateTemp = (DateTime) courtTimeSlot.Date;
    //                 var timeTemp = (TimeSpan) courtTimeSlot.Time;
    //                 dateTemp = dateTemp.Add(timeTemp);
    //                 timeCheck.Add(dateTemp);
    //             }
    //         }
    //         
    //         
    //         
    //         foreach (var courtTimeSlot in courtTimeSlots)
    //         {
    //             courtTimeSlot.Id = 
    //         }
    //         
    //         var res = baseRepository.InsertMany(courtTimeSlots);
    //         return CreateServiceResult(Success: res > 0, StatusCode: res > 0 ? 201 : 400);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Them du lieu court time slot bi loi", DevMsg: "Them du lieu bi loi");
    //     }
    // }
}