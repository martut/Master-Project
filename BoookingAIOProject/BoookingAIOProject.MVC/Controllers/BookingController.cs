using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BoookingAIOProject.DTO;
using BoookingAIOProject.MVC.Helpers;
using BoookingAIOProject.MVC.Models;
using BoookingAIOProject.MVC.ViewModel;
using Newtonsoft.Json;

namespace BoookingAIOProject.MVC.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public async Task<ActionResult> Index()
        {
            var client = BookingHttpClient.GetClient();


            var booking = await client.GetAsync("api/bookings");

            if (booking.IsSuccessStatusCode)
            {
                string content = await booking.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IEnumerable<Booking>>(content);

                return View(model);
            }

            return Content("Error");
        }

        public ActionResult RoomRedirect(int id)
        {
            return RedirectToAction("Edit", "Room", new { id });
        }

        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var booking = await client.GetAsync("api/booking/" + id);


                if (booking.IsSuccessStatusCode)
                {
                    string bookingContent = await booking.Content.ReadAsStringAsync();
                    var bookingModel = JsonConvert.DeserializeObject<Booking>(bookingContent);

                    var room = await client.GetAsync("api/room/" + bookingModel.RoomId);

                    var roomItems = await client.GetAsync("api/roomitem/items/" + bookingModel.RoomId);

                    if (roomItems.IsSuccessStatusCode && room.IsSuccessStatusCode)
                    {
                        string roomContent = await room.Content.ReadAsStringAsync();
                        string roomItemsContent = await roomItems.Content.ReadAsStringAsync();

                        var roomModel = JsonConvert.DeserializeObject<Room>(roomContent);
                        var roomItemModel = JsonConvert.DeserializeObject<IEnumerable<RoomItem>>(roomItemsContent);

                        var viewModel = new BookingViewModel()
                        {
                            Room = roomModel,
                            RoomItems = roomItemModel,
                            Booking = bookingModel
                        };
                        return View(viewModel);
                    }
                }

                return Content("Error");
            }
            catch (Exception)
            {
                return Content("Error");
            }
        }

        public async Task<ActionResult> CheckIn(int id)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var booking = await client.GetAsync("api/booking/" + id);

                var canceledBookingStatusId = 2;

                if (booking.IsSuccessStatusCode)
                {
                    var response = await BookingUpdateResponse.BookingUpdate(id, booking, canceledBookingStatusId, client);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return Content("Error");
            }
            catch (Exception)
            {
                return Content("Error");
            }
        }


        public async Task<ActionResult> BookingCancelAsync(int id)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var booking = await client.GetAsync("api/booking/" + id);

                var canceledBookingStatusId = 5;

                if (booking.IsSuccessStatusCode)
                {
                    var response = await BookingUpdateResponse.BookingUpdate(id, booking, canceledBookingStatusId, client);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return Content("Error");
            }
            catch (Exception)
            {
                return Content("Error");
            }
        }


        public async Task<ActionResult> CheckOut(int id)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var booking = await client.GetAsync("api/bookingwithroom/" + id);

                var roomService = await client.GetAsync("api/roomservice/booking/" + id);

                if (booking.IsSuccessStatusCode && roomService.IsSuccessStatusCode)
                {
                    string bookingContent = await booking.Content.ReadAsStringAsync();
                    string roomServiceContent = await roomService.Content.ReadAsStringAsync();
                    var bookingModel = JsonConvert.DeserializeObject<Booking>(bookingContent);
                    var roomServceModel = JsonConvert.DeserializeObject<IEnumerable<RoomService>>(roomServiceContent);

                    var vm = new CheckOutViewModel()
                    {
                        RoomServices = roomServceModel,
                        Booking = bookingModel
                    };

                    var checkout = new CheckOut();

                    vm = checkout.CalculateCheckOut(vm);

                    return View(vm);




                }
            }
            catch (Exception)
            {
                return Content("Error");
            }

            return View();
        }

        public async Task<ActionResult> CheckOutConfirmed(int id)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var booking = await client.GetAsync("api/booking/" + id);

                var checkOutBookingStatusId = 3;

                if (booking.IsSuccessStatusCode)
                {
                    var response = await BookingUpdateResponse.BookingUpdate(id, booking, checkOutBookingStatusId, client);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return Content("Error");
            }
            catch (Exception)
            {
                return Content("Error");
            }

        }

        public ActionResult Create()
        {
            var vm = new CreateBookingViewModel();
            return View(vm);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Create")]
        public async Task<ActionResult> Create(CreateBookingViewModel vm)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var bookings = await client.GetAsync("api/booking");
                var rooms = await client.GetAsync("api/room");
                if (bookings.IsSuccessStatusCode && rooms.IsSuccessStatusCode)
                {
                    var bookingsContent = await bookings.Content.ReadAsStringAsync();
                    var roomsContent = await rooms.Content.ReadAsStringAsync();
                    var bookingList = JsonConvert.DeserializeObject<IEnumerable<Booking>>(bookingsContent);
                    var roomsList = JsonConvert.DeserializeObject<IEnumerable<Room>>(roomsContent);

                    var roomSelection = new RoomSelection();
                    var sRoom = roomSelection.SelectRoom(bookingList, roomsList, vm.ChInDateTime, vm.ChOutDateTime,
                        vm.Booking.NumberOfAdults, vm.Booking.NumberOfChildren);

                    vm.Booking.RoomId = sRoom.Id;
                    vm.Booking.BookingStatusId = 1;
                    vm.Booking.CheckInDate = vm.ChInDateTime;
                    vm.Booking.CheckOutDate = vm.ChOutDateTime;

                    var bookingSerialize = JsonConvert.SerializeObject(vm.Booking);

                    var response = await client.PostAsync("api/booking",
                        new StringContent(bookingSerialize,
                            System.Text.Encoding.Unicode,
                            "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var bookingContent = await response.Content.ReadAsStringAsync();
                        var bookingModel = JsonConvert.DeserializeObject<Booking>(bookingContent);

                        return RedirectToAction("Update", new { id= bookingModel.Id});
                    }
                }

                return Content("Error");
            }
            catch (Exception)
            {

                return Content("Error");
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SelectRoom")]
        public async Task<ActionResult> SelectRoom(CreateBookingViewModel vm)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var bookings = await client.GetAsync("api/booking");
                var rooms = await client.GetAsync("api/room");
                if (bookings.IsSuccessStatusCode && rooms.IsSuccessStatusCode)
                {
                    var bookingsContent = await bookings.Content.ReadAsStringAsync();
                    var roomsContent = await rooms.Content.ReadAsStringAsync();
                    var bookingList = JsonConvert.DeserializeObject<IEnumerable<Booking>>(bookingsContent);
                    var roomsList = JsonConvert.DeserializeObject<IEnumerable<Room>>(roomsContent);

                    var roomSelection = new RoomSelection();
                    var sRoom = roomSelection.SelectRoom(bookingList, roomsList, vm.ChInDateTime, vm.ChOutDateTime, vm.Booking.NumberOfAdults, vm.Booking.NumberOfChildren);

                    vm.Booking.Room = sRoom;
                    vm.Booking.RoomId = sRoom.Id;

                    var roomItems = await client.GetAsync("api/roomitem/items/" + sRoom.Id);
                    if (roomItems.IsSuccessStatusCode)
                    {
                        var roomItemsContent = await roomItems.Content.ReadAsStringAsync();
                        var roomItemsList = JsonConvert.DeserializeObject<IEnumerable<RoomItem>>(roomItemsContent);

                        vm.RoomItems = roomItemsList;

                        return View("Create", vm);
                    }
                }

                return Content("Error");


            }
            catch (Exception)
            {
                return Content("Error");
            }
        }



    }

}

