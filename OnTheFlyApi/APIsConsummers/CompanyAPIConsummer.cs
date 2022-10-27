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
            using (HttpClient _companyClient = new HttpClient())
            {
                HttpResponseMessage response = await _companyClient.GetAsync($"https://localhost:44306/api/Company/GetCNPJ/{cnpj}/");
                var companyJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<Company>(companyJson);
                else return null;
            }
        }
        
        public static async Task<RestrictedCompany> GetOneRestrictedCNPJ(string cnpj)
        {
            using (HttpClient _restritedCompanyClient = new HttpClient())
            {
                HttpResponseMessage response = await _restritedCompanyClient.GetAsync($"https://localhost:44306/api/RestritedComapany/GetCNPJ/{cnpj}/");
                var companyJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<RestrictedCompany>(companyJson);
                else return null;
            }
        }
    }
}
