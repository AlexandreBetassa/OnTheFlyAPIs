using System.Net.Http;
using System.Threading.Tasks;

namespace ViaCepApi.Services
{
    public class ViaCepService
    {
        public async Task<string> GetAdress(string cep)
        {
            using (HttpClient _adressClient = new HttpClient())
            {
                HttpResponseMessage response = await _adressClient.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                var adress = await response.Content.ReadAsStringAsync();
                return adress;
            }
        }
    }
}
