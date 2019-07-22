using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.DTO
{
    public class BookingStatus
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
