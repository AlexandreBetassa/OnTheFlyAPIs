using AirCraftAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Models;
using System;

namespace AirCraftAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirCraftController : ControllerBase
    {
        private readonly AirCraftService _airCraftService;
        private readonly DeletedAirCraftService _deletedAirCraftService;
        public AirCraftController(AirCraftService airCraftService, DeletedAirCraftService deletedAirCraftService)
        {
            _airCraftService = airCraftService;
            _deletedAirCraftService = deletedAirCraftService;
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

        //[HttpPut] //Editar generico
        //public ActionResult<AirCraft> Update(AirCraft aircraftUpdate, string rab)
        //{
        //    var aircraftUpdate = _airCraftService.GetOneByRAB(rab);
        //    if (aircraftUpdate == null)
        //        return NotFound();

        //    _airCraftService.Update(aircraftUpdate, rab);

        //    return NoContent();
        //}
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpPut("ModifyAirCraftCapacity/{rab},{newCapacity}")]
        public ActionResult<AirCraft> UpdateCapacity(string rab, int newCapacity)
        {
            var aircraftUpdate = _airCraftService.GetOneByRAB(rab);
            if (aircraftUpdate == null)
                return NotFound();

            aircraftUpdate.Capacity = newCapacity;

            _airCraftService.Update(aircraftUpdate, rab);

            return NoContent();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpPut("ModifyAirCraftDtLastFlight/{rab},{updateLastFlight}")]
        public ActionResult<AirCraft> UpdateCapacity(string rab, DateTime updateLastFlight)
        {
            var aircraftUpdate = _airCraftService.GetOneByRAB(rab);
            if (aircraftUpdate == null)
                return NotFound();

            aircraftUpdate.DtLastFlight = updateLastFlight;

            _airCraftService.Update(aircraftUpdate, rab);

            return NoContent();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------








    }
}
