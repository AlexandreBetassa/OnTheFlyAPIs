using Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIsConsummers
{
    public class FlightAPIConsummer
    {
        public static async Task<Flight> GetFlight(SaleDTO sale)
        {
            using (HttpClient flightClient = new HttpClient())
            {
                string saleJson = JsonSerializer.Serialize(sale);
                HttpContent content = new StringContent(saleJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await flightClient.PostAsync($"https://localhost:44348/api/Flight/GetOne/", content);
                string flightJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Flight flight = JsonSerializer.Deserialize<Flight>(flightJson);
                    return flight;
                }
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
