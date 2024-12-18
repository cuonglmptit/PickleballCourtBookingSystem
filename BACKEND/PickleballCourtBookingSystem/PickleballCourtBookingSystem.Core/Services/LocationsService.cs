using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PickleballCourtBookingSystem.Core.DTOs;
using PickleballCourtBookingSystem.Core.Interfaces.Services;

namespace PickleballCourtBookingSystem.Core.Services
{
    public class LocationsService : ILocationsService
    {
        private readonly IHostEnvironment _env;

        // Constructor nhận IHostEnvironment để lấy thư mục gốc của ứng dụng
        public LocationsService(IHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Lấy danh sách các tỉnh thành của Việt Nam từ file JSON
        /// </summary>
        public async Task<ServiceResult> GetAllProvinces()
        {
            try
            {
                var solutionRootPath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, ".."));

                // Kết hợp với đường dẫn tới provinces.json trong thư mục Resources/Data
                var provincesJsonFilePath = Path.Combine(solutionRootPath, "PickleballCourtBookingSystem.Core", "Resources", "Data", "provinces.json");
                // Đọc file JSON
                var jsonData = await File.ReadAllTextAsync(provincesJsonFilePath);

                var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonData);

                // Trả về kết quả
                return new ServiceResult
                {
                    Success = true,
                    StatusCode = 200,
                    Data = provinces,
                    UserMsg = "Lấy danh sách tỉnh thành thành công",
                    DevMsg = "Danh sách tỉnh thành được lấy từ file JSON."
                };
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi đọc file JSON
                return new ServiceResult
                {
                    Success = false,
                    StatusCode = 500,
                    UserMsg = "Có lỗi xảy ra khi lấy danh sách tỉnh thành",
                    DevMsg = ex.Message
                };
            }
        }
        /// <summary>
        /// Lấy danh sách các quận huyện của Việt Nam từ file JSON
        /// </summary>
        public async Task<ServiceResult> GetAllDistricts()
        {
            try
            {
                var solutionRootPath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, ".."));

                // Kết hợp với đường dẫn tới provinces.json trong thư mục Resources/Data
                var provincesJsonFilePath = Path.Combine(solutionRootPath, "PickleballCourtBookingSystem.Core", "Resources", "Data", "districts.json");
                // Đọc file JSON
                var jsonData = await File.ReadAllTextAsync(provincesJsonFilePath);

                var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonData);

                // Trả về kết quả
                return new ServiceResult
                {
                    Success = true,
                    StatusCode = 200,
                    Data = provinces,
                    UserMsg = "Lấy danh sách quận huyện",
                    DevMsg = "Danh sách quận huyện được lấy từ file JSON."
                };
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi đọc file JSON
                return new ServiceResult
                {
                    Success = false,
                    StatusCode = 500,
                    UserMsg = "Có lỗi xảy ra khi lấy danh sách quận huyện",
                    DevMsg = ex.Message
                };
            }
        }/// <summary>
         /// Lấy danh sách các xã phường của Việt Nam từ file JSON
         /// </summary>
        public async Task<ServiceResult> GetAllWards()
        {
            try
            {
                var solutionRootPath = Path.GetFullPath(Path.Combine(_env.ContentRootPath, ".."));

                // Kết hợp với đường dẫn tới provinces.json trong thư mục Resources/Data
                var provincesJsonFilePath = Path.Combine(solutionRootPath, "PickleballCourtBookingSystem.Core", "Resources", "Data", "wards.json");
                // Đọc file JSON
                var jsonData = await File.ReadAllTextAsync(provincesJsonFilePath);

                var provinces = JsonConvert.DeserializeObject<List<Province>>(jsonData);

                // Trả về kết quả
                return new ServiceResult
                {
                    Success = true,
                    StatusCode = 200,
                    Data = provinces,
                    UserMsg = "Lấy danh sách xã phường",
                    DevMsg = "Danh sách quận xã phường lấy từ file JSON."
                };
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi đọc file JSON
                return new ServiceResult
                {
                    Success = false,
                    StatusCode = 500,
                    UserMsg = "Có lỗi xảy ra khi lấy danh sách xã phường",
                    DevMsg = ex.Message
                };
            }
        }
    }
}
