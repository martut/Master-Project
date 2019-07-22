using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BoookingAIOProject.DTO;
using Newtonsoft.Json;

namespace BoookingAIOProject.MVC.Models
{
    public static class BookingUpdateResponse
    {
        internal static async Task<HttpResponseMessage> BookingUpdate(int id, HttpResponseMessage booking, int changeBookingStatusId, HttpClient client)
        {
            var bookingModel = await DeserializeBooking(booking);

            bookingModel.BookingStatusId = changeBookingStatusId;

            return await SerializeBooking(id, client, bookingModel);
        }

        private static async Task<HttpResponseMessage> SerializeBooking(int id, HttpClient client, Booking bookingModel)
        {
            var serializeBooking = JsonConvert.SerializeObject(bookingModel);
            var response = await client.PutAsync("api/booking/" + id,
                new StringContent(serializeBooking, Encoding.Unicode, "application/json"));
            return response;
        }

        private static async Task<Booking> DeserializeBooking(HttpResponseMessage booking)
        {
            var bookingContent = await booking.Content.ReadAsStringAsync();
            var bookingModel = JsonConvert.DeserializeObject<Booking>(bookingContent);
            return bookingModel;
        }
    }
}