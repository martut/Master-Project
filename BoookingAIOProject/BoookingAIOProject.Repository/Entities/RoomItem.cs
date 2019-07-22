using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.Repository.Entities
{
    public class RoomItem
    {
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int  ItemId { get; set; }

        public virtual Room Room { get; set; }
        public virtual Item Item { get; set; }
    }
}
