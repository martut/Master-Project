using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoookingAIOProject.Repository.Entities;

namespace BoookingAIOProject.Repository.Factories
{
    public class BookingStatusFactory
    {
        public BookingStatus CreateBookingStatus(DTO.BookingStatus bookingStatus)
        {
            return new BookingStatus()
            {
                Id = bookingStatus.Id,
                IsActive = bookingStatus.IsActive,
                Label = bookingStatus.Label,
                Order = bookingStatus.Order
            };
        }
        public DTO.BookingStatus CreateBookingStatus(BookingStatus bookingStatus)
        {
            return new DTO.BookingStatus()
            {
                Id = bookingStatus.Id,
                IsActive = bookingStatus.IsActive,
                Label = bookingStatus.Label,
                Order = bookingStatus.Order
            };
        }
    }
}
