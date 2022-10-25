using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIViaCep
{
    public class ViaCep
    {
        public ViaCep() { }
        public async Task<Address> GetAdress(string cep)
        {
            Address adress;
            using (HttpClient _adressClient = new HttpClient())
            {
                HttpResponseMessage response = await _adressClient.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                var adressJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return adress = JsonConvert.DeserializeObject<Address>(adressJson);
                else return null;
            }
        }
    }
}
