using System;

namespace BoookingAIOProject.DTO
{
    public class RoomService
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int RoomServiceTypeId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public RoomServiceType RoomServiceType { get; set; }

    }
}