using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.Repository.Entities
{
    public class BookingStatus
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Label { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
