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

        public static async Task<Passenger> PostPassenger(Passenger passenger)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                HttpContent http = new StringContent(passenger.ToString());
                http.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await _passengerClient.PostAsync($"https://localhost:44355/api/Passenger/Create", http);

                var passengerJson = await response.Content.ReadAsStringAsync();
                return null;

                /*            
                string requestUrl = endpointUri + "/Files/";
                var jsonString = JsonConvert.SerializeObject(new { name = "newFile.txt", type = "File" }); 

                HttpContent httpContent = new StringContent(jsonString);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue ("application/json");  
                */
            }
        }
    }
}
