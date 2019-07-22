using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.ViewModel
{
    public class BookingViewModel
    {
        public Booking Booking { get; set; }

        public Room Room { get; set; }
        public IEnumerable<RoomItem> RoomItems { get; set; }

    }
}