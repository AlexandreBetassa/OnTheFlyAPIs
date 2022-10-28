using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace APIsConsummers
{
    public class PassengersAPIConsummer
    {
        //exemplo de get
        public static async Task<List<Passenger>> GetPassengers()
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                HttpResponseMessage response = await _passengerClient.GetAsync($"https://localhost:44355/api/Passenger/"); //insere endpoint como no exemplo
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<List<Passenger>>(passengerJson);
                else return null;
            }
        }

        ////exemplo de post
        //public static async Task<bool> PostPassenger(Passenger passenger)
        //{
        //    using (HttpClient _passengerClient = new HttpClient())
        //    {
        //        string jsonString = JsonConvert.SerializeObject(passenger);
        //        HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:44355/api/Passenger/Create/", http);

        //        if (response.IsSuccessStatusCode) return true;
        //        return false;
        //    }
        //}
    }
}
