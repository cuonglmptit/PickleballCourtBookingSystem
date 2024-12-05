using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtService : BaseService<Court>, ICourtService
{

    private readonly ICourtRepository _courtRepository;
    private readonly ICourtTimeSlotService _courtTimeSlotService;

    public CourtService(ICourtRepository repository, ICourtRepository courtRepository,
        ICourtTimeSlotService courtTimeSlotService) : base(repository)
    {
        _courtRepository = courtRepository;
        _courtTimeSlotService = courtTimeSlotService;
    }

    public ServiceResult GetCourtForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            var totalHour = (int)(endTime - startTime).TotalHours;
            var listCourtTimeSlot = (IEnumerable<CourtTimeSlot>)_courtTimeSlotService.GetAvailableCourtTimeSlotForTimeRange(date, startTime, endTime).Data!;
            var groupByCourt = listCourtTimeSlot.GroupBy(c => c.CourtId);
            var listCourt = new List<Court>();
            foreach (var group in groupByCourt)
            {
                if (group.Count() != totalHour) continue;
                var court = _courtRepository.GetById(group.Key);
                if (court == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court not found", DevMsg: $"Court not found with id {group.Key}");
                }

                listCourt.Add(court);
            }

            return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }
    
    public ServiceResult GetAvailableCourtsForTime(DateTime date, TimeSpan startTime, TimeSpan endTime)
    {
        try
        {
            var listCourtTimeSlot = (IEnumerable<CourtTimeSlot>)_courtTimeSlotService.GetAvailableCourtTimeSlotForTimeRange(date, startTime, endTime).Data!;
            var courtIds = new HashSet<Guid>();
            foreach (var courtTimeSlot in listCourtTimeSlot)
            {
                if (courtTimeSlot.CourtId == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Court id la null", DevMsg: $"Court id la null");
                }
                courtIds.Add(courtTimeSlot.CourtId.Value);
            }
            var listCourt = new List<Court>();
            foreach (var courtId in courtIds)
            {
                var court = _courtRepository.GetById(courtId);
                if (court == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court not found",
                        DevMsg: $"Court not found with id courtId");
                }
                listCourt.Add(court);
            }

            return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourt);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
        }
    }
}
