using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViaCepApi.Services;
using Models;

namespace ViaCepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaCepController : ControllerBase
    {
        private readonly ViaCepService _viaCepService;
        public ViaCepController()
        {
            _viaCepService = new ViaCepService();
        }

        [HttpGet("{cep}")]
        public Address GetAdress(string cep)
        {
            var adress = _viaCepService.GetAdress(cep).Result;
            if (adress == null) return null;
            return ;
        }


    }
}
