using PickleballCourtBookingSystem.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickleballCourtBookingSystem.Core.Entities;

namespace PickleballCourtBookingSystem.Core.Interfaces.Infrastructure
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
