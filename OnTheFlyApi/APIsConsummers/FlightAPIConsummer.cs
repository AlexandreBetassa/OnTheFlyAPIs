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

        public static async Task<Passenger> GetFlight(DateTime fullDate, string rabPlane, string destiny)
        {
            using (HttpClient flightClient = new HttpClient())
            {
                HttpResponseMessage response = await flightClient.GetAsync($"https://localhost:44348/api/Flight/GetOne/{fullDate}/{rabPlane}/{destiny}/");
                var flightJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Passenger>(flightJson);
                else return null;
            }
        }
    }
}
