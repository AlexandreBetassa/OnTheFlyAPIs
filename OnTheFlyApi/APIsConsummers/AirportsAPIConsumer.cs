using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using System.Threading.Tasks;

namespace APIsConsummers
{
    internal class AirportsAPIConsumer
    {
        private static readonly string _localhost = "https://localhost";
        private static readonly string _port = "6000";

        public async static Task<List<Airport>> GetAllAirports()
        {
            using (HttpClient clientAirport = new HttpClient())
            {
                var response = await clientAirport.GetAsync($"{_localhost}:{_port}");//colocar endpoint correto
                if (response.IsSuccessStatusCode)
                {
                    string airportJson;
                    airportJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Airport>>(airportJson);
                }
                else return null;
            }
        }

        public async static Task<Airport> GetByIata(string iata)
        {
            using (HttpClient clientAirport = new HttpClient())
            {
                var response = await clientAirport.GetAsync($"{_localhost}:{_port}/{iata}");//colocar endpoint correto
                if (response.IsSuccessStatusCode)
                {
                    string airportJson;
                    airportJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Airport>(airportJson);
                }
                else return null;
            }
        }

        public async static Task<List<Airport>> GetByState(string state)
        {
            using (HttpClient clientAirport = new HttpClient())
            {
                var response = await clientAirport.GetAsync($"{_localhost}:{_port}/{state}");//colocar endpoint correto
                if (response.IsSuccessStatusCode)
                {
                    string airportJson;
                    airportJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Airport>>(airportJson);
                }
                else return null;
            }
        }

        public async static Task<List<Airport>> GetByCountryId(string countryId)
        {
            using (HttpClient clientAirport = new HttpClient())
            {
                var response = await clientAirport.GetAsync($"{_localhost}:{_port}");//colocar endpoint correto
                if (response.IsSuccessStatusCode)
                {
                    string airportJson;
                    airportJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Airport>>(airportJson);
                }
                else return null;
            }
        }

        public async static Task<List<Airport>> GetByCityCode(string cityId)
        {
            using (HttpClient clientAirport = new HttpClient())
            {
                var response = await clientAirport.GetAsync($"{_localhost}:{_port}");//colocar endpoint correto
                if (response.IsSuccessStatusCode)
                {
                    string airportJson;
                    airportJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Airport>>(airportJson);
                }
                else return null;
            }
        }
    }
}
