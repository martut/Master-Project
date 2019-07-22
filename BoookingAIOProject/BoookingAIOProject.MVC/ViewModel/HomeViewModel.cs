using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Booking> CheckInBooking { get; set; }
        public IEnumerable<Booking> CheckOutBookings { get; set; }
        public IEnumerable<Booking> LastBookings { get; set; }
        public int RoomNumer { get; set; }
        public Booking Booking { get; set; }
        public IEnumerable<SelectListItem> RoomServices { get; set; }
        public RoomService RoomService { get; set; }
        public int RoomServiceTypeId { get; set; }
    }
}