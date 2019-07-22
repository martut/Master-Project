using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.Repository.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string GuestLastName { get; set; }
        [Required]
        [StringLength(50)]
        public string GuestFirstName { get; set; }
        [Required]
        public DateTime CheckInDate { get; set; }
        [Required]
        public DateTime CheckOutDate { get; set; }
        [Required]
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int BookingStatusId { get; set; }

        public virtual Room Room { get; set; }
        public virtual BookingStatus BookingStatus { get; set; }
    }
}
