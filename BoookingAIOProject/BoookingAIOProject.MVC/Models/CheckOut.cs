using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BoookingAIOProject.DTO;
using BoookingAIOProject.MVC.ViewModel;
using Microsoft.Owin.Security;

namespace BoookingAIOProject.MVC.Models
{
    public class CheckOut
    {
        public CheckOutViewModel CalculateCheckOut(CheckOutViewModel checkOutViewModel)
        {
            var booking = checkOutViewModel.Booking;
            var roomServices = checkOutViewModel.RoomServices;

            var daysOfStay = TimeSpan(booking);

            var payForDays = PayForDays(booking, daysOfStay);

            var payForServices = PayForServices(roomServices);

            var allToPay = payForDays + payForServices;

            checkOutViewModel.PayForDays = payForDays;
            checkOutViewModel.TotalPay = allToPay;
            checkOutViewModel.DaysOfStay = daysOfStay;

            return checkOutViewModel;
        }

        private double PayForServices(IEnumerable<RoomService> roomServices)
        {
            double total = 0;

            foreach (var service in roomServices)
            {
                total += service.Amount * service.RoomServiceType.Price;
            }

            return total;
        }

        private double PayForDays(Booking booking, int daysOfStay)
        {
            return booking.Room.Price * daysOfStay;
        }

        private int TimeSpan(Booking booking)
        {
            TimeSpan span = booking.CheckOutDate.Subtract(booking.CheckInDate);
            return (int)span.TotalDays;
        }
    }
}