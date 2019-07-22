using System;
using System.ComponentModel.DataAnnotations;

namespace BoookingAIOProject.Repository.Entities
{
    public class RoomService
    {
        public int Id { get; set; }
        [Required]
        public int BookingId { get; set; }
        [Required]
        public int RoomServiceTypeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int Amount { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual RoomServiceType RoomServiceType { get; set; }
        
    }
}