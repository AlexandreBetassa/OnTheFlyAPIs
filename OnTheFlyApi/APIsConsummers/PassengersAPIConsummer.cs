using Models;
using Nancy.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using System.Text;

namespace APIsConsummers
{
    public class PassengersAPIConsummer
    {
        //Get a single passenger object
        public static async Task<Passenger> GetPassenger(string unformattedCpf)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:44355/api/Passenger/GetByCPF/{unformattedCpf}");
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return new JavaScriptSerializer().Deserialize<Passenger>(passengerJson);
                else return null;
            }
        }

        //Get the passengers list
        public static async Task<List<Passenger>> GetPassengerList()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
                    await client.GetAsync($"https://localhost:44355/api/Passenger/GetAll");
                var passengerJsonArray = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<List<Passenger>>(passengerJsonArray);
                else return null;
            }
        }

        //Get the restricted passengers list
        public static async Task<List<RestrictedPassenger>> GetRestrictedPassengerList()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
                    await client.GetAsync($"https://localhost:44355/api/RestrictedPassenger/GetAll");
                var restrictedPassengerJsonArray = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return JsonSerializer.Deserialize<List<RestrictedPassenger>>(restrictedPassengerJsonArray);
                else return null;
            }
        }

        public static async Task<List<Passenger>> PostListPassenger(List<String> lstPassengerDTO)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                string jsonString = JsonSerializer.Serialize(lstPassengerDTO);
                HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:44355/api/Passenger/Create/", http); //alterar endpoint com o correto
                var PassengerJsonArray = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonSerializer.Deserialize<List<Passenger>>(PassengerJsonArray);
                return null;
            }
        }
    }
}
