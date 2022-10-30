using Models;
using Nancy.Json;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
                    return JsonConvert.DeserializeObject<List<Passenger>>(passengerJsonArray);
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
                    return JsonConvert.DeserializeObject<List<RestrictedPassenger>>(restrictedPassengerJsonArray);
                else return null;
            }
        }

        //Post example
        //public static async Task<bool> PostPassenger(Passenger passenger, DateTime data)
        //{
        //    using (HttpClient _passengerClient = new HttpClient())
        //    {
        //        string jsonString = JsonConvert.SerializeObject(data);
        //        HttpContent http = new StringContent(jsonString, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:44355/api/Passenger/Create/{data}", http);

        //        if (response.IsSuccessStatusCode) return true;
        //        return false;
        //    }
        //}
    }
}
