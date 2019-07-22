using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.Models
{
    public class HomeListsCreation
    {
        public IEnumerable<Booking> CheckInList(IEnumerable<Booking> bookingList)
        {
            var list = bookingList
                .Where(b => b.CheckInDate.Date == DateTime.Now.Date)
                .Where(b => b.BookingStatusId == 1 || b.BookingStatusId == 2)
                .OrderBy(b => b.GuestLastName);

            return list;
        }
        public IEnumerable<Booking> CheckOutList(IEnumerable<Booking> bookingList)
        {
            var list = bookingList
                .Where(b => b.CheckOutDate.Date == DateTime.Now.Date)
                .Where(b => b.BookingStatusId == 2 || b.BookingStatusId == 3)
                .OrderBy(b => b.GuestLastName);

            return list;
        }
        public IEnumerable<Booking> LastList(IEnumerable<Booking> bookingList)
        {
            var list = bookingList
                .OrderByDescending(b => b.Id)
                .Take(4);

            return list;
        }


    }
}