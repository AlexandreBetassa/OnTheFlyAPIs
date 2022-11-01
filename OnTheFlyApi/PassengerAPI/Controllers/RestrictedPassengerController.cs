using Microsoft.AspNetCore.Mvc;
using Models;
using PassengerAPI.Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PassengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictedPassengerController : ControllerBase
    {
        private readonly RestrictedPassengerService _restrictedPassengerService;
        private readonly PassengerService _passengerService;

        public RestrictedPassengerController(RestrictedPassengerService restrictedPassengerService, PassengerService passengerService)
        {
            _restrictedPassengerService = restrictedPassengerService;
            _passengerService = passengerService;
        }

        // GET: api/<RestrictedPassengerController>
        [HttpGet("GetAll")]
        public ActionResult<List<RestrictedPassenger>> Get()
        {
            return Ok(_restrictedPassengerService.Get());
        }

        // GET api/<RestrictedPassengerController>/5
        [HttpGet("GetByCPF/{unformattedCpf}")]
        public ActionResult<RestrictedPassenger> Get(string unformattedCpf)
        {
            var restrictedPassenger = _restrictedPassengerService.Get(Models.Utils.FormatCPF(unformattedCpf));
            if (restrictedPassenger == null) return NotFound();
            return Ok(restrictedPassenger);
        }

        // POST api/<RestrictedPassengerController>
        [HttpPost("Create")]
        public ActionResult<RestrictedPassenger> Post(RestrictedPassengerDTO r)
        {
            if (!Models.Utils.CPFIsValid(r.UnformattedCPF)) return BadRequest("Invalid CPF.");

            string formattedCpf = Models.Utils.FormatCPF(r.UnformattedCPF);

            if (_restrictedPassengerService.Get(formattedCpf) != null) return Unauthorized("Passenger already exists.");

            var passenger = _passengerService.Get(formattedCpf);
            if (passenger != null)
            {
                passenger.Status = true;
                _passengerService.Replace(passenger.CPF, passenger);
            }
            
            return Ok(_restrictedPassengerService.Create(new() { CPF = formattedCpf}));
        }


        // DELETE api/<RestrictedPassengerController>/5
        [HttpDelete("DeleteByCPF/{unformattedCpf}")]
        public ActionResult<RestrictedPassenger> Delete(string unformattedCpf)
        {
            string formattedCpf = Models.Utils.FormatCPF(unformattedCpf);

            var restrictedPassenger = _restrictedPassengerService.Remove(formattedCpf);
            if (restrictedPassenger == null) return NotFound();

            var passenger = _passengerService.Get(formattedCpf);
            if (passenger != null)
            {
                passenger.Status = false;
                _passengerService.Replace(passenger.CPF, passenger);
            }

            return Ok(restrictedPassenger);
        }
    }
}
