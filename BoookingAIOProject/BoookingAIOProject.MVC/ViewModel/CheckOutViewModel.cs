using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.ViewModel
{
    public class CheckOutViewModel
    {
        public Booking Booking { get; set; }
        public IEnumerable<RoomService> RoomServices { get; set; }

        public double PayForDays { get; set; }
        public double TotalPay { get; set; }
        public int DaysOfStay { get; set; }
        

    }
}