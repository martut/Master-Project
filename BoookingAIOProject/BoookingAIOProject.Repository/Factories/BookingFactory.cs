using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class BookingFactory
    {
     
        RoomFactory RoomFactory = new RoomFactory();
        BookingStatusFactory BookingStatusFactory = new BookingStatusFactory();

        public Booking CreateBooking(DTO.Booking booking)
        {
            return new Booking()
            {
                Id = booking.Id,
                GuestFirstName = booking.GuestFirstName,
                GuestLastName = booking.GuestLastName,
                BookingStatusId = booking.BookingStatusId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfAdults = booking.NumberOfAdults,
                NumberOfChildren = booking.NumberOfChildren,
                RoomId = booking.RoomId,
                Room = booking.Room == null ? null :  RoomFactory.CreateRoom(booking.Room),
                BookingStatus = booking.BookingStatus == null ? null : BookingStatusFactory.CreateBookingStatus(booking.BookingStatus)
            };
        }
        public DTO.Booking CreateBooking(Booking booking)
        {
            return new DTO.Booking()
            {
                Id = booking.Id,
                GuestFirstName = booking.GuestFirstName,
                GuestLastName = booking.GuestLastName,
                BookingStatusId = booking.BookingStatusId,
                CheckInDate = booking.CheckInDate,
                CheckOutDate = booking.CheckOutDate,
                NumberOfAdults = booking.NumberOfAdults,
                NumberOfChildren = booking.NumberOfChildren,
                RoomId = booking.RoomId,
                Room = booking.Room == null ? null : RoomFactory.CreateRoom(booking.Room),
                BookingStatus = booking.BookingStatus == null ? null : BookingStatusFactory.CreateBookingStatus(booking.BookingStatus)
            };
        }
    }
}
