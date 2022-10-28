﻿using AirCraftAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Models;
using System;
using System.Net;
using APIsConsummers;

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
        //public ActionResult<List<AirCraft>> GetAllByCnpj(string companyCnpj)
        //{
        //    //var aircraftList = _airCraftService.GetAllByCnpj(companyCnpj);
        //    //return aircraftList;
        //}
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
        public ActionResult<AirCraft> CreateAirCraft(AirCraft airCraftInsert)
        {
            //   ----> VALIDAÇÕES A SEREM FEITAS AQUI   <----   //

            // PRECISA ANTES DE FAZER A INSERCAO, VERIFICAR SE A COMPANHIA AEREA INFORMADA REALMENTE EXISTE CADASTRADA E SE

            //var company = CompanyAPIConsummer.GetOneCNPJ()

            // primeiro verifica se o RAB informado é valido:
            //
            //
            //
            var airCraft = _airCraftService.GetOneByRAB(airCraftInsert.RAB);
            if (airCraft != null)
               return StatusCode((int)HttpStatusCode.Conflict, "Could not proceed with this request. There is already an aircraft registered with this RAB code!");


            // O RAB INFORMADO JÁ NÃO ESTÁ CADASTRADO
            // ADICIONAR SYSTEMDATETIME.NOW NO CADASTRO

            _airCraftService.Create(airCraftInsert);

            return Ok(airCraftInsert);
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpPut("ModifyAirCraftCapacity/{rab}/{newCapacity}")]
        public ActionResult<AirCraft> UpdateCapacity(string rab, int newCapacity)
        {
            var aircraftUpdate = _airCraftService.GetOneByRAB(rab);
            if (aircraftUpdate == null)
                return NotFound();

            aircraftUpdate.Capacity = newCapacity;

            _airCraftService.UpdateCapacity(aircraftUpdate, rab);

            return NoContent();
        }


        //[HttpPut("ModifyAirCraftDtLastFlight/{aircraftUpdate}")]   //update usando o objeto completo ja atualizado
        //public ActionResult<AirCraft> UpdateLastFlight(AirCraft aircraftUpdate)
        //{
        //    _airCraftService.Update(aircraftUpdate);

        //    return NoContent();
        //}

        [HttpPut("ModifyAirCraftDtLastFlight/{rab}/{updateLastFlight}")]
        public ActionResult<AirCraft> UpdateLastFlight(string rab, DateTime updateLastFlight)
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

        [HttpDelete("RemoveAirCraft/{rab}")]
        public ActionResult<AirCraft> DeleteAirCraft(string rab)
        {
            var airCraft = _airCraftService.GetOneByRAB(rab);
            if (airCraft == null)
                return NotFound();

            _deletedAirCraftService.Insert(airCraft);

            _airCraftService.Remove(airCraft);

            return NoContent();
        }



    }
}
