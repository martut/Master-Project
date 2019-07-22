using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.Repository.Entities
{
    public class Room
    {
        public int Id { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public int AdultsCapacity { get; set; }
        [Required]
        public int ChildrenCapacity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
