using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.DTO
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int AdultsCapacity { get; set; }
        public int ChildrenCapacity { get; set; }
        public double Price { get; set; }
    }
}
