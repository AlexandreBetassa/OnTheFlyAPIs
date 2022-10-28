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
                HttpResponseMessage response = await _airCraftClient.GetAsync($"https://localhost:44311/api/AirCraft/GetByRAB/{rab}/");
                var airCraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<AirCraft>(airCraftJson);
                else return null;
            }
        }

        public static async Task<AirCraft> UpdateLastFlight(string rab, DateTime updateLastFlight)
        {
            using (HttpClient _airCraftClient = new HttpClient())
            {
                HttpResponseMessage response = await _airCraftClient.GetAsync($"https://localhost:44311/api/AirCraft/ModifyAirCraftDtLastFlight/{rab}/{updateLastFlight}/");
                var airCraftJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<AirCraft>(airCraftJson);
                else return null;
            }
        }

        //public static async Task<AirCraft> CreateAirCraft(AirCraft airCraft)
        //{
        //    using (HttpClient _airCraftClient = new HttpClient())
        //    {
        //        HttpResponseMessage response = await _airCraftClient.GetAsync($"https://localhost:44311/api/{airCraft}/");////
        //        var airCraftJson = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<AirCraft>(airCraftJson);
        //        else return null;
        //    }
        //}
    }
}