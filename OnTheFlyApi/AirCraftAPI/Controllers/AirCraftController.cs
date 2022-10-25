using AirCraftAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Models;

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

        //-----------------------------------------------------------------------------------------------------------------
        //Get All 
        [HttpGet]
        public ActionResult<List<AirCraft>> GetAll() => _airCraftService.GetAll();
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        //Get All By CNPJ
        [HttpGet("GetByCnpj/{companyCnpj}")]
        public ActionResult<List<AirCraft>> GetAllByCnpj(string companyCnpj)
        {
            var aircraftList = _airCraftService.GetAllByCnpj(companyCnpj);

            return aircraftList;
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        //Get One By RAB
        [HttpGet("GetByRAB/{rab}")]
        public ActionResult<AirCraft> GetByRAB(string rab)
        {
            var airCraft = _airCraftService.GetOneByRAB(rab);
            if (airCraft == null)
                return NotFound();

            return airCraft;
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public ActionResult<AirCraft> CreateAirCraft(AirCraft aircraft)
        {
            //   ----> VALIDAÇÕES A SEREM FEITAS AQUI   <----   //

            // PRECISA ANTES DE FAZER A INSERCAO, VERIFICAR SE A COMPANHIA AEREA INFORMADA REALMENTE EXISTE CADASTRADA E SE
            // O RAB INFORMADO JÁ NÃO ESTÁ CADASTRADO

            _airCraftService.Create(aircraft);

            return Ok(aircraft);
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------







    }
}
