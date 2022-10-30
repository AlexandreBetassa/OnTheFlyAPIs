using Models;
using System.Text.Json;
using System;
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
                HttpResponseMessage response = await flightClient.GetAsync($"https://localhost:44348/api/Flight/GetOne/{fullDate}/{rabPlane.ToUpper()}/{destiny.ToUpper()}/");
                var flightJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonSerializer.Deserialize<Flight>(flightJson);
                else return null;
            }
        }

        public static async Task<bool> UpdateFlightSales(Flight flight)
        {
            using (HttpClient _flightClient = new HttpClient())
            {
                string flightJson = JsonSerializer.Serialize(flight);
                HttpContent flightClientContent = new StringContent(flightJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _flightClient.PutAsync($"https://localhost:44348/api/Flight/ModifyFlightSales", flightClientContent);

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }
    }
}
