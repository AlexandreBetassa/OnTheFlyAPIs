using Models;
using Nancy.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System;
using System.Text;

namespace APIsConsummers
{
    public class PassengersAPIConsummer
    {
        //Get passengers for a sale
        public static async Task<List<Passenger>> GetSalePassengersList(List<string> unformattedCpfList)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(unformattedCpfList);
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:44355/api/Passenger/GetSalePassengersList", content);
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<List<Passenger>>(passengerJson);
                else return null;
            }
        }
    }
}
