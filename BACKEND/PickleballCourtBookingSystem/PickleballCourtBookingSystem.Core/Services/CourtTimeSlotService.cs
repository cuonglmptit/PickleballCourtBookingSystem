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

    public ServiceResult GetCourtTimeSlotsByCourtId(Guid courtId)
    {
        try
        {
            var res = _courtTimeSlotRepository.FindByColumnValue(courtId, "CourtId");
            return CreateServiceResult(Success: true, StatusCode: 200, Data: res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public ServiceResult GetCourtTimeSlot(Guid? courtId)
    {
        try
        {
            var res = _courtTimeSlotRepository.FindCourtTimeSlots(courtId);
            return CreateServiceResult(Success: true, StatusCode: 200, Data: res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}