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

        public RestrictedPassengerController(RestrictedPassengerService restrictedPassengerService)
        {
            _restrictedPassengerService = restrictedPassengerService;
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
        public ActionResult<RestrictedPassenger> Post(string cpf)
        {
            var restrictedPassenger = _restrictedPassengerService.Get(cpf);
            if (restrictedPassenger != null) return Unauthorized();

            restrictedPassenger = new() { CPF = cpf, };
            return _restrictedPassengerService.Create(restrictedPassenger);
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
