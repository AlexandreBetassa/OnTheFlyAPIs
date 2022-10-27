using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Models;
using Newtonsoft.Json;

namespace APIsConsummers
{
    public class CompanyAPIConsummer
    {
        public static async Task<Company> GetOneCNPJ(string cnpj)
        {
            using (HttpClient _passengerClient = new HttpClient())
            {
                HttpResponseMessage response = await _passengerClient.GetAsync($"https://localhost:44306/api/Company/GetCNPJ/{cnpj}/");
                var passengerJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Company>(passengerJson);
                else return null;
            }
        }
    }
}
