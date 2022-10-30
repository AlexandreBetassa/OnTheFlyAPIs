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
    public class AirCraftAPIConsummer
    {
        public static async Task<AirCraft> GetAirCraft(string rab)
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                HttpResponseMessage response = await _airCraftClient.GetAsync($"https://localhost:44311/api/AirCraft/GetByRAB/{rab}");
                var airCraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<AirCraft>(airCraftJson);
                else return null;
            }
        }


        public static async Task<bool> PostAirCraft(AirCraftDTO aircraft)
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(aircraft);
                HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _airCraftClient.PostAsync($"https://localhost:44311/api/AirCraft/", http);

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }


        //public static async Task<bool> UpdateAirCraft(string rab, DateTime updateLastFlight)
        //{
        //    using (HttpClient _airCraftClient = new HttpClient())
        //    {
        //        string jsonString = JsonConvert.SerializeObject(updateLastFlight);
        //        HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _airCraftClient.PostAsync($"https://localhost:44311/api/AirCraft/ModifyAirCraftDtLastFlight/{rab}/{updateLastFlight}", http);

        //        if (response.IsSuccessStatusCode) return true;
        //        return false;
        //    }
        //}

        public static async Task<bool> UpdateAirCraft(AirCraft aircraft)  /// tentativa de dar update com o objeto completo ja atualizado
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                string jsonString = JsonConvert.SerializeObject(aircraft);
                HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _airCraftClient.PutAsync("https://localhost:44311/api/AirCraft/ModifyAirCraftDtLastFlight", http); // alterar endpoint!

                if (response.IsSuccessStatusCode) return true;
                return false;
            }
        }
    }
}