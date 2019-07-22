using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoookingAIOProject.DTO
{
    public class Booking
    {
        public int Id { get; set; }
        public string GuestLastName { get; set; }
        public string GuestFirstName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int RoomId { get; set; }
        public int BookingStatusId { get; set; }

        public Room Room { get; set; }
        public BookingStatus BookingStatus { get; set; }


    }
}
