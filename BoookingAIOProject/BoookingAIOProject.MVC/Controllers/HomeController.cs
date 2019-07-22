using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BoookingAIOProject.DTO;
using BoookingAIOProject.MVC.Helpers;
using BoookingAIOProject.MVC.Models;
using BoookingAIOProject.MVC.ViewModel;
using Newtonsoft.Json;
using WebGrease.Css.Ast.Selectors;

namespace BoookingAIOProject.MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var bookings = await client.GetAsync("api/bookings");
                var services = await client.GetAsync("api/roomservicetype");
                if (bookings.IsSuccessStatusCode && services.IsSuccessStatusCode)
                {
                    var bookingsContent = await bookings.Content.ReadAsStringAsync();
                    var bookingsList = JsonConvert.DeserializeObject<IEnumerable<Booking>>(bookingsContent);
                    var servicesContent = await services.Content.ReadAsStringAsync();
                    var servicesList = JsonConvert.DeserializeObject<IEnumerable<RoomServiceType>>(servicesContent);

                    var dropdownList = new SelectList(servicesList, "Id", "Name");


                    var homeListCreation = new HomeListsCreation();
                    var bookingList = bookingsList.ToList();
                    var vm = new HomeViewModel
                    {
                        CheckInBooking = homeListCreation.CheckInList(bookingList),
                        CheckOutBookings = homeListCreation.CheckOutList(bookingList),
                        LastBookings = homeListCreation.LastList(bookingList),
                        RoomServices = dropdownList
                    };
                    return View(vm);


                }

                return Content("Error");
            }
            catch (Exception)
            {
                return Content("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(HomeViewModel viewModel)
        {
            var client = BookingHttpClient.GetClient();

            var room = await client.GetAsync("api/room/number/" + viewModel.RoomNumer);

            if (room.IsSuccessStatusCode)
            {
                var bookings = await client.GetAsync("api/bookings");
                var services = await client.GetAsync("api/roomservicetype");


                if (bookings.IsSuccessStatusCode && services.IsSuccessStatusCode)
                {
                    var roomContent = await room.Content.ReadAsStringAsync();
                    var bookingsContent = await bookings.Content.ReadAsStringAsync();
                    var roomModel = JsonConvert.DeserializeObject<Room>(roomContent);
                    var bookingList = JsonConvert.DeserializeObject<IEnumerable<Booking>>(bookingsContent);
                    var today = DateTime.Now.Date;

                    var bookingModel = bookingList
                        .Where(b => b.CheckInDate <= today &&
                                    today <= b.CheckOutDate)
                        .Where(b => b.BookingStatusId == 2)
                        .FirstOrDefault(b => b.RoomId == roomModel.Id);

                    var servicesContent = await services.Content.ReadAsStringAsync();
                    var servicesList = JsonConvert.DeserializeObject<IEnumerable<RoomServiceType>>(servicesContent);

                    var dropdownList = new SelectList(servicesList, "Id", "Name");


                    var homeListCreation = new HomeListsCreation();

                    var bookingForFilter = bookingList.ToList();
                    var vm = new HomeViewModel
                    {
                        CheckInBooking = homeListCreation.CheckInList(bookingForFilter),
                        CheckOutBookings = homeListCreation.CheckOutList(bookingForFilter),
                        LastBookings = homeListCreation.LastList(bookingForFilter),
                        Booking = bookingModel,
                        RoomNumer = viewModel.RoomNumer,
                        RoomServices = dropdownList
                    };

                    return View(vm);
                }
            }

            return Content("Error");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}