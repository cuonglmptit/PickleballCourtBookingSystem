﻿using PickleballCourtBookingSystem.Api.Models;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Entities;
using PickleballCourtBookingSystem.Core.Interfaces.Infrastructure;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services

{
    public class CourtClusterService : BaseService<CourtCluster>, ICourtClusterService
    {
        private readonly ICourtClusterRepository _courtClusterRepository;
        private readonly ICourtService _courtService;
        private readonly ICourtPriceService _courtPriceService;
        private readonly ICourtTimeSlotService _courtTimeSlotService;
        private readonly IAddressService _addressService;
        public CourtClusterService(ICourtClusterRepository repository, ICourtClusterRepository courtClusterRepository, ICourtService courtService, ICourtPriceService courtPriceService, ICourtTimeSlotService courtTimeSlotService, IAddressService addressService) : base(repository)
        {
            _courtClusterRepository = courtClusterRepository;
            _courtService = courtService;
            _courtPriceService = courtPriceService;
            _courtTimeSlotService = courtTimeSlotService;
            _addressService = addressService;
        }

        public ServiceResult GetCourtClustersForTimeRange(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                var resultGetCourt = _courtService.GetCourtForTimeRange(date, startTime, endTime);
                if (!resultGetCourt.Success || resultGetCourt.Data == null)
                {
                    return resultGetCourt;
                }
                var listCourt = (List<Court>)_courtService.GetCourtForTimeRange(date, startTime, endTime).Data!;
                var listCourtCluster = new List<CourtCluster>();
                var courtClusterIds = new HashSet<Guid>();
                foreach (var court in listCourt)
                {
                    if (court.CourtClusterId == null)
                    {
                        return CreateServiceResult(false, StatusCode: 500, UserMsg: "id bi null", DevMsg: "court cluster id trong bang court la null");
                    }

                    courtClusterIds.Add(court.CourtClusterId.Value);
                }
                foreach (var courtClusterId in courtClusterIds)
                {
                    var courtCluster = _courtClusterRepository.GetById(courtClusterId);

                    if (courtCluster == null)
                    {
                        return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court cluster not found",
                            DevMsg: "Court cluster not found");
                    }
                    listCourtCluster.Add(courtCluster);
                }
                return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtCluster);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult GetAvailableCourtClusterForTime(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                var resultGetCourt = _courtService.GetCourtForTimeRange(date, startTime, endTime);
                if (!resultGetCourt.Success || resultGetCourt.Data == null)
                {
                    return resultGetCourt;
                }
                var listCourt = (List<Court>)_courtService.GetAvailableCourtsForTime(date, startTime, endTime).Data!;
                var listCourtCluster = new List<CourtCluster>();
                var courtClusterIds = new HashSet<Guid>();
                foreach (var court in listCourt)
                {
                    if (court.CourtClusterId == null)
                    {
                        return CreateServiceResult(false, StatusCode: 500, UserMsg: "id bi null", DevMsg: "court cluster id trong bang court la null");
                    }

                    courtClusterIds.Add(court.CourtClusterId.Value);
                }
                foreach (var courtClusterId in courtClusterIds)
                {
                    var courtCluster = _courtClusterRepository.GetById(courtClusterId);

                    if (courtCluster == null)
                    {
                        return CreateServiceResult(Success: false, StatusCode: 404, UserMsg: "Court cluster not found",
                            DevMsg: "Court cluster not found");
                    }
                    listCourtCluster.Add(courtCluster);
                }
                return CreateServiceResult(Success: true, StatusCode: 200, Data: listCourtCluster);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }

        public ServiceResult SearchCourtClusterWithFilters(string? cityName, string? courtClusterName,
            DateTime? date, TimeSpan? startTime, TimeSpan? endTime)
        {
            try
            {
                var timeNow = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
                var dateCheck = date ?? timeNow.Date;
                //Nếu ngày tìm lớn hơn ngày hôm nay thì thời gian bắt đầu sẽ là 00:00
                //, nếu ngày tìm mà là hôm nay thì thời gian bắt đầu sẽ là hiện tại, 
                Console.WriteLine("startTime truyen vao: " + startTime);
                var startTimeCheck = startTime ?? TimeSpan.Zero;
                if (dateCheck.Date == timeNow.Date && !startTime.HasValue)
                {
                    startTimeCheck = timeNow.TimeOfDay;
                }
                var endTimeCheck = endTime ?? DateTime.Today.AddDays(1).AddTicks(-1).TimeOfDay;
                //Bắt đầu kiểm tra
                if (dateCheck < timeNow.Date)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngày truyền vào trong quá khứ", DevMsg: "Ngày truyền vào trong quá khứ");
                }
                if (timeNow.Date == dateCheck && (startTimeCheck < timeNow.TimeOfDay || endTimeCheck < timeNow.TimeOfDay))
                {
                    Console.WriteLine(timeNow.Date == dateCheck);
                    Console.WriteLine(startTimeCheck < timeNow.TimeOfDay);
                    Console.WriteLine(endTimeCheck < timeNow.TimeOfDay);
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Thời gian bắt đầu hoặc kết thúc truyền vào trong quá khứ", DevMsg: "Thời gian bắt đầu hoặc kết thúc truyền vào trong quá khứ");
                }
                if (startTimeCheck > endTimeCheck)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Thời gian kết thúc lớn hơn thời gian bắt đầu", DevMsg: "Thời gian kết thúc lớn hơn thời gian bắt đầu");
                }
                if (dateCheck.Date < timeNow.Date)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Ngày truyền vào trong quá khứ", DevMsg: "Ngày truyền vào trong quá khứ");
                }
                var result = (List<CourtCluster>)_courtClusterRepository.SearchCourtClusterWithFilters(cityName, courtClusterName, dateCheck, startTimeCheck, endTimeCheck);
                Console.WriteLine("service SearchCourtClusterWithFilters kết quả: " + result.Count);

                if (result.Count == 0)
                {
                    return CreateServiceResult(Success: true, StatusCode: 200, UserMsg: "Khong co cum san san thoa man dieu kien", DevMsg: "Khong co san thoa man dieu kien");
                }
                return CreateServiceResult(Success: true, StatusCode: 200, Data: result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Query loi", DevMsg: "Loi");
            }
        }

        public ServiceResult AddTimeSlotsWithDefaultPrice(Guid courtClusterId, List<DateTime> dates)
        {
            try
            {
                if (dates.Count == 0)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400, UserMsg: "Danh sach ngay khong duoc de trong", DevMsg: "Danh sach ngay khong duoc de trong");
                }
                var resultListCourtPrice = _courtPriceService.GetCourtPricesByCourtClusterId(courtClusterId);
                if (!resultListCourtPrice.Success)
                {
                    return resultListCourtPrice;
                }

                var resultListCourt = _courtService.GetCourtsByCourtClusterId(courtClusterId);
                if (!resultListCourt.Success)
                {
                    return resultListCourt;
                }

                if (resultListCourt.Data == null)
                {
                    return CreateServiceResult(Success: false, StatusCode: 400,
                        UserMsg: "Cum san khong co san nao", DevMsg: "Cum san khong co san nao");
                }
                var listCourtPrice = (IEnumerable<CourtPrice>)resultListCourtPrice.Data!;
                var listCourt = (IEnumerable<Court>)resultListCourt.Data!;
                var courtIds = new List<Guid>();
                foreach (var court in listCourt)
                {
                    if (!court.Id.HasValue) return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: "courtClusterId trong court la null");
                    courtIds.Add(court.Id.Value);
                }
                var result = _courtTimeSlotService.AddCourtTimeSlotWithDefaultPrice(courtIds, dates, listCourtPrice);
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }
        
        public ServiceResult GetCourtsByClusterId(Guid courtClusterId)
        {
            try
            {
                var result = _courtService.GetCourtsByCourtClusterId(courtClusterId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return CreateServiceResult(Success: false, StatusCode: 500, UserMsg: "Error", DevMsg: e.Message);
            }
        }
    }
}