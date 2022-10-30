using Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;

namespace APIsConsummers
{
    public class ViaCepAPIConsummer
    {
        public static async Task<Address> GetAdress(string cep)
        {
            using (HttpClient _adressClient = new HttpClient())
            {
                HttpResponseMessage response = await _adressClient.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                var adressJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Address address = JsonConvert.DeserializeObject<Address>(adressJson);
                    return address; 
                }
                else return null;
            }
        }
    }
}
