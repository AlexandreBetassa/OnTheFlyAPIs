using AirCraftAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Models;
using APIViaCep;

namespace AirCraftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftController : ControllerBase
    {
        private readonly AirCraftService _airCraftService;
        public AirCraftController(AirCraftService airCraftService)
        {
            _airCraftService = airCraftService;
        }

        [HttpGet]
        public ActionResult<List<AirCraft>> Get() => _airCraftService.Get();

        [HttpGet("{cep}")]
        public ActionResult<Address> GetAdress(string cep)
        {
            var address = ViaCep.GetAdress(cep).Result;

            return Ok(address);
        }


        [HttpGet("GetByAirCraftRAB/{rab}")]
        public ActionResult<AirCraft> GetByRAB(string rab)
        {
            var airCraft = _airCraftService.Get(rab);
            if (airCraft == null)
                return NotFound();

            return airCraft;
        }




    }
}
