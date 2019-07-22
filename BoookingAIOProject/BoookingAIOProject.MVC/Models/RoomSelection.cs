using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoookingAIOProject.DTO;

namespace BoookingAIOProject.MVC.Models
{
    public class RoomSelection
    {
        public Room SelectRoom(IEnumerable<Booking> bookingList, IEnumerable<Room> roomsList, DateTime ChIn, DateTime ChOut, int adlt, int child )
        {
            var selectedBookings = bookingList
                .Where(b => ((ChIn >= b.CheckInDate && ChIn < b.CheckOutDate) ||
                             (ChOut > b.CheckInDate && ChOut <= b.CheckOutDate)));

            foreach (var selectedBooking in selectedBookings)
            {
                var something = selectedBooking.Id;
            }
            var selectedRooms = roomsList
                .Where(r => r.AdultsCapacity >= adlt)
                .Where(r => r.AdultsCapacity + r.ChildrenCapacity >= adlt + child)
                .Where(r => !selectedBookings.Any(b => b.RoomId == r.Id))
                .OrderBy(r=> r.Price)
                .FirstOrDefault();



            return selectedRooms;


        }

    }
}