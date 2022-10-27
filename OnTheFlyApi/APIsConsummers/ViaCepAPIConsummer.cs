using Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIsConsummers
{
    public class ViaCepAPIConsummer
    {
        public static async Task<Address> GetAdress(string cep)
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
