using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services;

public class CourtTimeSlotService : BaseService<CourtTimeSlot>, ICourtTimeSlotService
{
    public CourtTimeSlotService(ICourtTimeSlotRepository repository) : base(repository)
    {
        
    }

    public ServiceResult CheckTimeSlotAvailableAndCheckPrice(CourtTimeSlot courtTimeSlot)
    {
        var courtTimeSlotCheck = baseRepository.GetById(courtTimeSlot.Id);
        if (courtTimeSlotCheck == null)
        {
            return CreateServiceResult(false, StatusCode: 404, UserMsg: "Course time slot not found", DevMsg: "Course time slot not found");
        }
        if (courtTimeSlotCheck.Price == null || courtTimeSlot.Price == null)
        {
            return CreateServiceResult(false, StatusCode: 404, UserMsg: "Course time slot not found", DevMsg: "Course time slot not found");
        }

        if (courtTimeSlot.Price.HasValue && courtTimeSlotCheck.Price.HasValue)
        {
            var roundedPriceCheck = (decimal) Math.Round(courtTimeSlotCheck.Price.Value, 2);
            var roundedPriceInput = (decimal) Math.Round(courtTimeSlot.Price.Value, 2);
            if (courtTimeSlotCheck.IsAvailable == 1 && roundedPriceInput == roundedPriceCheck)
            {
                return CreateServiceResult(true, StatusCode: 200, Data: true);
            }
        }
        return CreateServiceResult(false, StatusCode: 200, UserMsg: "Course time slot is not available", DevMsg: "Course time slot is not available", Data: false);
    }
}