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
    public class PassengersAPIConsummer
    {
        public static async Task<Passenger> GetPassenger(string cpf)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                HttpResponseMessage response = await _passengerClient.GetAsync($"https://localhost:44355/api/Passenger/{cpf}/"); //insere endpoint como no exemplo
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Passenger>(passengerJson);
                else return null;
            }
        }
    }
}
