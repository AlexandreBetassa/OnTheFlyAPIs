using APIViaCep;
using Microsoft.AspNetCore.Mvc;
using Models;
using PassengerAPI.Services;
using System.Collections.Generic;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using System.Runtime.ConstrainedExecution;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PassengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passengerService;

        public PassengerController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        // GET: api/<PassengerController>
        [HttpGet]
        public ActionResult<List<Passenger>> Get()
        {
            return _passengerService.Get();
        }

        // GET api/<PassengerController>/5
        [HttpGet("GetByCPF/{cpf}")]
        public ActionResult<Passenger> Get(string cpf)
        {
            var passenger = _passengerService.Get(cpf);
            if (passenger == null) return NotFound();
            return passenger;
        }

        // POST api/<PassengerController>
        [HttpPost]
        public ActionResult<Passenger> Post(Passenger passenger)
        {
            var address = ViaCep.GetAdress(passenger.Address.ZipCode).Result;
            if (address == null) return NotFound();
            address.Number = passenger.Address.Number;
            address.Complement = passenger.Address.Complement;
            passenger.Address = address;
            return _passengerService.Create(passenger);
        }

        // PUT api/<PassengerController>/5
        [HttpPut]
        //public ActionResult<Passenger> Put(Passenger passengerIn, string cpf)
        //{
        //    var passenger = _passengerService.Get(cpf);
        //    if (passenger == null) return NotFound();
        //    passengerIn.CPF = cpf;
        //    _passengerService.Replace(cpf, passengerIn);
        //    return NoContent();
        //}

        // DELETE api/<PassengerController>/5
        [HttpDelete("{cpf}")]
        public ActionResult<Passenger> Delete(string cpf)
        {
            var passenger = _passengerService.Get(cpf);
            if (passenger == null) return NotFound();
            _passengerService.Remove(passenger);
            return NoContent();
        }
    }
}
