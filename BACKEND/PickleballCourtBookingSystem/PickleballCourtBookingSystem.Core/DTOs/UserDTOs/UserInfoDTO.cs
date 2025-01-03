using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleballCourtBookingSystem.Core.DTOs.UserDTOs
{
    public class UserInfoDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }
}
