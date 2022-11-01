using Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace APIsConsummers
{
    public class PassengersAPIConsummer
    {
        //Get passengers for a sale
        public static async Task<Passenger> GetSalePassengersList(string cpf)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                HttpResponseMessage response = await _passengerClient.GetAsync($"https://localhost:44355/api/Passenger/GetByCPF/{cpf}");
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonSerializer.Deserialize<Passenger>(passengerJson);
                else return null;
            }
        }
    }
}
