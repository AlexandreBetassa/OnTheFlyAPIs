using AirCraftAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Models;
using System;
using System.Net;
using APIsConsummers;
using System.Threading.Tasks;

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
            var cnpj = companyCnpj.Replace(".", "").Replace("/", "").Replace("-", "");
            if (cnpj.Length != 14) return BadRequest("CNPJ is not valid. CNPJ needs to be 14 characters long, without formatation.");
            if (!long.TryParse(cnpj, out _)) return BadRequest("CNPJ is not valid. Use Only Numbers.");
            cnpj = Utils.FormatCNPJ(companyCnpj);

            var aircraftList = _airCraftService.GetAllByCnpj(cnpj);
            return aircraftList;
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        //Get One By RAB
        [HttpGet("GetByRAB/{rab}")]
        public ActionResult<AirCraft> GetByRAB(string rab)
        {
            rab = rab.ToUpper();
            string rabValidation = Utils.ValidateRab(rab);
            if (rabValidation != "OK") return BadRequest(rabValidation);

            var airCraft = _airCraftService.GetOneByRAB(rab);
            if (airCraft == null)
                return NotFound();

            return airCraft;
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpPost]
        public async Task<ActionResult<AirCraft>> CreateAirCraft([FromBody] AirCraft airCraftInsert)
        {
            airCraftInsert.RAB = airCraftInsert.RAB.ToUpper();
            var cnpj = airCraftInsert.Company.CNPJ.Replace(".", "").Replace("/", "").Replace("-", "");
            //-----------------------------------------------
            if (cnpj.Length != 14) return BadRequest("CNPJ is not valid. CNPJ needs to be 14 characters long, without formatation.");
            if (!long.TryParse(cnpj, out _)) return BadRequest("CNPJ is not valid. Use Only Numbers.");

            string rabValidation = Utils.ValidateRab(airCraftInsert.RAB);
            if (rabValidation != "OK") return BadRequest(rabValidation);

            var airCraft = _airCraftService.GetOneByRAB(airCraftInsert.RAB);
            if (airCraft != null)
                return StatusCode((int)HttpStatusCode.Conflict, "Could not proceed with this request. There is already an aircraft registered with this RAB code!");



            var company = await CompanyAPIConsummer.GetOneCNPJ(cnpj);
            if (company == null) return BadRequest("Invalid CNPJ. Could not found an company with informed CNPJ.");

            airCraftInsert.Company = company;

            _airCraftService.Create(airCraftInsert);

            return Ok(airCraftInsert);
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpPut("ModifyAirCraftCapacity/{rab}/{newCapacity}")]
        public ActionResult<AirCraft> UpdateCapacity(string rab, int newCapacity)
        {
            rab = rab.ToUpper();
            string rabValidation = Utils.ValidateRab(rab);
            if (rabValidation != "OK") return BadRequest(rabValidation);

            var aircraftUpdate = _airCraftService.GetOneByRAB(rab);
            if (aircraftUpdate == null)
                return NotFound();

            if (newCapacity < 1 || newCapacity > 999) return BadRequest("Aircraft Capacity must have a numeric value Integer between 1 to 999.");

            aircraftUpdate.Capacity = newCapacity;

            _airCraftService.UpdateCapacity(aircraftUpdate, rab);

            return NoContent();
        }


        [HttpPut("ModifyAirCraftDtLastFlight/")]
        public ActionResult<AirCraft> UpdateLastFlight(AirCraft aircraftUpdate)
        {
            aircraftUpdate.RAB = aircraftUpdate.RAB.ToUpper();

            string rabValidation = Utils.ValidateRab(aircraftUpdate.RAB);
            if (rabValidation != "OK") return BadRequest(rabValidation);

            _airCraftService.Update(aircraftUpdate);

            return NoContent();
        }
        //-----------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------

        [HttpDelete("RemoveAirCraft/{rab}")]
        public ActionResult<AirCraft> DeleteAirCraft(string rab)
        {
            rab = rab.ToUpper();
            string rabValidation = Utils.ValidateRab(rab);
            if (rabValidation != "OK") return BadRequest(rabValidation);

            var airCraft = _airCraftService.GetOneByRAB(rab);
            if (airCraft == null)
                return NotFound();

            _deletedAirCraftService.Insert(airCraft);

            _airCraftService.Remove(airCraft);

            return NoContent();
        }

    }
}
