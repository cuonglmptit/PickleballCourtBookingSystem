using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PickleballCourtBookingSystem.Core.CustomValidation
{
    public class CustomValidationMethods
    {
        /// <summary>
        /// Check xem có phải email hợp lệ hay không
        /// Author: CuongLM (08/08/2024)
        /// </summary>
        /// <param name="email">email muốn kiểm tra</param>
        /// <returns>true-email hợp lệ, false-email không hợp lệ</returns>
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check xem ngày có ở trong quá khứ không
        /// </summary>
        /// <param name="date">Ngày muốn kiểm tra</param>
        /// <returns>true-ngày trong quá khứ, false-ngày lớn hơn hiện tại</returns>
        /// Author: CuongLM (08/08/2024)
        public static bool IsDateInPast(DateTime? date)
        {
            if (date != null)
            {
                if (date > DateTime.Now)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Kiểm tra xem mã code có kết thúc bằng số hay không
        /// Author: CuongLM (08/08/2024)
        /// </summary>
        /// <param name="input">giá trị muốn kiểm tra</param>
        /// <returns>true-kết thúc bằng số, false-ngược lại</returns>
        public static bool EndsWithNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // Lấy ký tự cuối cùng của chuỗi
            char lastChar = input[input.Length - 1];

            // Kiểm tra xem ký tự cuối cùng có phải là chữ số không
            return char.IsDigit(lastChar);
        }

        /// <summary>
        /// CuongLM (21/12/2024)
        /// Check username co hợp lệ không
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>
        /// true - username hợp lệ
        /// false - username không hợp lệ
        /// </returns>
        public static bool IsValidUsername(string username)
        {
            // Kiểm tra nếu chuỗi rỗng hoặc null
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            // Kiểm tra độ dài username
            if (username.Length < 6 || username.Length > 36)
            {
                return false;
            }

            // Kiểm tra không có khoảng trắng trong username
            if (username.Contains(" "))
            {
                return false;
            }

            // Kiểm tra username chỉ chứa ký tự hợp lệ (chữ cái, số, dấu gạch dưới hoặc dấu gạch ngang)
            string pattern = @"^[a-zA-Z0-9_-]+$";
            if (!Regex.IsMatch(username, pattern))
            {
                return false;
            }

            // Kiểm tra không bắt đầu hoặc kết thúc bằng dấu gạch dưới hoặc dấu gạch ngang
            if (username.StartsWith("_") || username.StartsWith("-") || username.EndsWith("_") || username.EndsWith("-"))
            {
                return false;
            }

            return true; // Username hợp lệ
        }

        /// <summary>
        /// CuongLM (21/12/2024)
        /// Kiểm tra xem một chuỗi có phải là tên hợp lệ không
        /// </summary>
        /// <param name="fullName">
        /// tên cần kiểm tra
        /// </param>
        /// <returns>
        /// true - tên hợp lệ
        /// false - tên không hợp lệ
        /// </returns>
        public static bool IsValidFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            // Loại bỏ khoảng trắng thừa
            fullName = fullName.Trim();

            // Chiều dài phải nằm trong khoảng cho phép
            if (fullName.Length < 2 || fullName.Length > 100)
                return false;

            // Kiểm tra bằng regex: chỉ chứa chữ cái, khoảng trắng, dấu gạch nối hoặc dấu nháy đơn
            string pattern = @"^[a-zA-ZÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸỳỵýỷỹ\s'-]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(fullName, pattern);
        }

    }
}
