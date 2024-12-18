using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickleballCourtBookingSystem.Core.DTOs;

namespace PickleballCourtBookingSystem.Core.Interfaces.Services
{
    /// <summary>
    /// Interface này không tương tác với database, chỉ là lấy ra các gợi ý/tỉnh thành của Việt Nam
    /// </summary>
    public interface ILocationsService
    {
        public Task<ServiceResult> GetAllProvinces();
        public Task<ServiceResult> GetAllDistricts();
        public Task<ServiceResult> GetAllWards();
    }

}
