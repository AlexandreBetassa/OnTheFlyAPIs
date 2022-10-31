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
        public static async Task<List<Passenger>> GetSalePassengersList(List<string> unformattedCpfList, string localPort)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(unformattedCpfList);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:{localPort}/api/Passenger/GetSalePassengersList", content);
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<List<Passenger>>(passengerJson);
                else return null;
            }
        }
    }
}
