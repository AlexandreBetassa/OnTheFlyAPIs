using Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace APIsConsummers
{
    public class PassengersAPIConsummer
    {
        //Get passengers for a sale
        public static async Task<List<Passenger>> GetSalePassengersList(List<string> unformattedCpfList)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                string jsonString = JsonSerializer.Serialize(unformattedCpfList);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:44355/api/Passenger/GetSalePassengersList", content);
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonSerializer.Deserialize<List<Passenger>>(passengerJson);
                else return null;
            }
        }
    }
}
