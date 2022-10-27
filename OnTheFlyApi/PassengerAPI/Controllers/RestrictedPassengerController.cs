using Microsoft.AspNetCore.Mvc;
using Models;
using PassengerAPI.Services;
using System.Collections.Generic;
using System;

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
            return _restrictedPassengerService.Get();
        }

        // GET api/<RestrictedPassengerController>/5
        [HttpGet("GetByCPF/{cpf}")]
        public ActionResult<RestrictedPassenger> Get(string cpf)
        {
            var restrictedPassenger = _restrictedPassengerService.Get(cpf);
            if (restrictedPassenger == null) return NotFound();
            return restrictedPassenger;
        }

        // POST api/<RestrictedPassengerController>
        [HttpPost("Create")]
        public ActionResult<RestrictedPassenger> Post(RestrictedPassengerDTO r)
        {
            if (_restrictedPassengerService.Get(r.CPF) != null) return Unauthorized();

            var passenger = _passengerService.Get(r.CPF);
            if (passenger != null)
            {
                passenger.Status = true;
                _passengerService.Replace(passenger.CPF, passenger);
            }
            
            return _restrictedPassengerService.Create(new() { CPF = r.CPF});
        }



        // DELETE api/<RestrictedPassengerController>/5
        [HttpDelete("DeleteByCPF/{cpf}")]
        public ActionResult<RestrictedPassenger> Delete(string cpf)
        {
            var restrictedPassenger = _restrictedPassengerService.Remove(cpf);
            if (restrictedPassenger == null)
                return NotFound();
            return restrictedPassenger;
        }
    }
}
