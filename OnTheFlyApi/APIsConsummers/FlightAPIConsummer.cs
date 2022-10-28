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

        public static async Task<bool> UpdateFlightSales(Flight flight)
        {
            using (HttpClient _flightClient = new HttpClient())
            {
                string flightJson = JsonConvert.SerializeObject(flight);
                HttpContent flightClientContent = new StringContent(flightJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _flightClient.PutAsync($"https://localhost:44348/api/Flight/ModifyFlightSales", flightClientContent);

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }
    }
}
