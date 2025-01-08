using PickleballCourtBookingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickleballCourtBookingSystem.Core.DTOs
{
    public class CourtClusterAdminDTO
    {
        public CourtCluster CourtCluster { get; set; }
        public List<Court> Courts { get; set; }
        public User Owner { get; set; }
    }
}
