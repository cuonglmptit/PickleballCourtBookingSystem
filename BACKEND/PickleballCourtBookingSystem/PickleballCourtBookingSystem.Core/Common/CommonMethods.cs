using Microsoft.AspNetCore.Http;
using PickleballCourtBookingSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleballCourtBookingSystem.Core.Common
{
    /// <summary>
    /// Class những method chung hay sử dụng
    /// </summary>
    public class CommonMethods
    {
        /// <summary>
        /// CuongLM (21/12/2024)
        /// Tạo 1 service result
        /// </summary>
        /// <param name="Success">Kết quả service</param>
        /// <param name="StatusCode">Status code</param>
        /// <param name="UserMsg">Message cho user</param>
        /// <param name="DevMsg">Message cho dev</param>
        /// <param name="Data">Data của service</param>
        /// <returns></returns>
        public static ServiceResult CreateServiceResult(bool Success, int StatusCode, string? UserMsg = null,
        string? DevMsg = null, object? Data = null)
        {
            ServiceResult result = new ServiceResult();
            result.Success = Success;
            result.UserMsg = UserMsg;
            result.DevMsg = DevMsg;
            result.StatusCode = StatusCode;
            result.Data = Data;
            return result;
        }

        /// <summary>
        /// Lấy token từ header
        /// </summary>
        /// <param name="context">Context</param>
        /// <returns>Token dưới dạng string</returns>
        public static string GetTokenFromHeader(HttpContext context)
        {
            try
            {
                if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                {
                    var token = authorizationHeader.ToString();
                    if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        return token["Bearer ".Length..].Trim();
                    }
                }
                return null; // Trả về null nếu không có token hợp lệ
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null; // Trả về null nếu không có token hợp lệ
            }
        }

    }
}
