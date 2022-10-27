using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIsConsummers
{
    public class FlightAPIConsummer
    {
        public static async Task<Flight> GetFlight(DateTime fullDate, string rabPlane, string destiny)
        {
            using (HttpClient flightClient = new HttpClient())
            {
                HttpResponseMessage response = await flightClient.GetAsync($"https://localhost:44348/api/Flight/GetOne/{fullDate}/{rabPlane}/{destiny}/");
                var flightJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Flight>(flightJson);
                else return null;
            }
        }

        public static async Task<bool> UpdateFlightSales(DateTime fullDate, string rabPlane, string destiny, int newSales)
        {
            using (HttpClient _flightClient = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(newSales);
                HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _flightClient.PutAsync($"https://localhost:44348/apiFlight//api/Flight/ModifyFlightSales/{fullDate}/{rabPlane}/{destiny}/{newSales}", http);

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }
    }
}
