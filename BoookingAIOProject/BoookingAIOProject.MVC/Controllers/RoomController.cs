using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BoookingAIOProject.DTO;
using BoookingAIOProject.MVC.Helpers;
using BoookingAIOProject.MVC.ViewModel;
using Newtonsoft.Json;

namespace BoookingAIOProject.MVC.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public async Task<ActionResult> Index()
        {
            var client = BookingHttpClient.GetClient();

            HttpResponseMessage response = await client.GetAsync("api/room");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IEnumerable<Room>>(content);

                return View(model);
            }
            else
            {
                return Content("An error occurred.");
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Room room)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var serializedItemToCreate = JsonConvert.SerializeObject(room);

                var response = await client.PostAsync("api/room",
                    new StringContent(serializedItemToCreate,
                        System.Text.Encoding.Unicode,
                        "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Error");
                }
            }
            catch
            {
                return Content("Error");
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            var client = BookingHttpClient.GetClient();


            var response = await client.GetAsync("api/room/" + id);

            var responseRoomItems = await client.GetAsync("api/roomitem/items/" + id);

            var responeItems = await client.GetAsync("api/item");

            if (response.IsSuccessStatusCode && responseRoomItems.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                string contentRoomItem = await responseRoomItems.Content.ReadAsStringAsync();
                string contentItem = await responeItems.Content.ReadAsStringAsync();

                var roomModel = JsonConvert.DeserializeObject<Room>(content);

                var roomItemsModel = JsonConvert.DeserializeObject<IEnumerable<RoomItem>>(contentRoomItem);



                var itemsModel = JsonConvert.DeserializeObject<IEnumerable<Item>>(contentItem);

                var dropdownList = new SelectList(itemsModel, "Id", "Name");


                var viewModel = new RoomEditViewModel()
                {
                    Room = roomModel,
                    RoomItems = roomItemsModel,
                    Items = dropdownList
                };


                return View(viewModel);
            }

            return Content("An error occurred");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoomEditViewModel model)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var serializedItemToUpdate = JsonConvert.SerializeObject(model.Room);

                var response = await client.PutAsync("api/room/" + model.Room.Id,
                    new StringContent(serializedItemToUpdate, System.Text.Encoding.Unicode,
                        "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Error");
                }

            }
            catch (Exception )
            {
                return Content("Error");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RoomItem(RoomEditViewModel model)
        {
            try
            {
                var client = BookingHttpClient.GetClient();

                var roomItem = new RoomItem()
                {
                    ItemId = model.SelectedItemId,
                    RoomId = model.Room.Id
                };

                var serializedItemToCreate = JsonConvert.SerializeObject(roomItem);

                var response = await client.PostAsync("api/roomitem",
                    new StringContent(serializedItemToCreate,
                        System.Text.Encoding.Unicode,
                        "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Edit", new { id = model.Room.Id});
                }
                else
                {
                    return Content("Error");
                }

            }
            catch (Exception)
            {
                return Content("Error");
            }
        }

        public async Task<ActionResult> Delete(int id, int roomId)
        {
            try
            {
                var client = BookingHttpClient.GetClient();
                var response = await client.DeleteAsync("api/roomitem/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Edit", new { id = roomId });
                }
                else
                {
                    return Content("Error");
                }
            }
            catch (Exception )
            {
                return Content("Error");
            }
        }
    }
}