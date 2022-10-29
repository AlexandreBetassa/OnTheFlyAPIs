using Models;
using System;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIsConsummers
{
    public class AirCraftAPIConsummer
    {
        public static async Task<AirCraft> GetAirCraft(string rab)
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                HttpResponseMessage response = await _airCraftClient.GetAsync($"https://localhost:44311/api/AirCraft/GetByRAB/{rab}");
                var airCraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonSerializer.Deserialize<AirCraft>(airCraftJson);
                else return null;
            }
        }


        public static async Task<bool> PostAirCraft(AirCraft aircraft)
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                string jsonString = JsonSerializer.Serialize(aircraft);
                HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _airCraftClient.PostAsync($"https://localhost:44311/api/AirCraft/", http);

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }


        public static async Task<bool> UpdateAirCraft(string rab, DateTime updateLastFlight)
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                string jsonString = JsonSerializer.Serialize(updateLastFlight);
                HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _airCraftClient.PostAsync($"https://localhost:44311/api/AirCraft/ModifyAirCraftDtLastFlight/{rab}/{updateLastFlight}", http);

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }

        //public static async Task<bool> UpdateAirCraft(AirCraft aircraft)  /// tentativa de dar update com o objeto completo ja atualizado
        //{
        //    using (HttpClient _airCraftClient = new HttpClient())
        //    {
        //        string jsonString = JsonSerializer.Serialize(aircraft);
        //        HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _airCraftClient.PostAsync($"https://localhost:44311/api/AirCraft/{aircraft}", http); // alterar endpoint!

        //        if (response.IsSuccessStatusCode) return true;
        //        return false;
        //    }
        //}
    }
}