using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BoookingAIOProject.MVC.Helpers
{
    public static class BookingHttpClient
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(Constants.BookingAPI)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            
            return client;
        }
    }
}