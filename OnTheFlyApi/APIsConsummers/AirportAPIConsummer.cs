using Models;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIsConsummers
{
    public class AirportAPIConsummer
    {
        public static async Task<Airport> GetAirport(string iata)
        {
            //using (HttpClient _airportClient = new HttpClient())
            //{
            //    HttpResponseMessage response = await _airportClient.GetAsync($"https://localhost:44366/Airport/{iata}");
            //    var airportJson = await response.Content.ReadAsStringAsync();
            //    if (response.IsSuccessStatusCode) return new JavaScriptSerializer().Deserialize<Airport>(airportJson);
            //    else return null;
            //}

            using (HttpClient _airportClient = new HttpClient())
            {

                HttpResponseMessage response = await _airportClient.GetAsync($"https://localhost:44366/Airport/{iata}");
                string airportJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Airport airport = JsonSerializer.Deserialize<Airport>(airportJson);
                    return airport;
                }
                else return null;
            }
        }
    }
}
