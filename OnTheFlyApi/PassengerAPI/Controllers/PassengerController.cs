using APIViaCep;
using Microsoft.AspNetCore.Mvc;
using Models;
using PassengerAPI.Services;
using System.Collections.Generic;
using System;
using APIsConsummers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PassengerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passengerService;
        private readonly RestrictedPassengerService _restrictedPassengerService;

        public PassengerController(PassengerService passengerService, RestrictedPassengerService restrictedPassengerService)
        {
            _passengerService = passengerService;
            _restrictedPassengerService = restrictedPassengerService;
        }

        // GET: api/<PassengerController>
        [HttpGet("GetAll")]
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
        [HttpPost("Create")]
        public ActionResult<Passenger> Post(PassengerDTO p)
        {
            if (_passengerService.Get(p.CPF) != null) return Unauthorized();

            var address = ViaCepAPIConsummer.GetAdress(p.Address.ZipCode).Result;
            if (address == null) return NotFound();

            Passenger passenger = new()
            {
                CPF = p.CPF,
                Name = p.Name.ToUpper(),
                Gender = p.Gender.ToUpper(),
                Phone = p.Phone,
                DtBirth = p.DtBirth,
                DtRegister = DateTime.Now,
                Status = false,
                Address = new Address
                {
                    ZipCode = address.ZipCode,
                    Street = address.Street,
                    Number = p.Address.Number,
                    Complement = p.Address.Complement.ToUpper(),
                    City = address.City.ToUpper(),
                    State = address.State.ToUpper()
                }
            };

            if (_restrictedPassengerService.Get(p.CPF) != null) passenger.Status = true;

            return _passengerService.Create(passenger);
        }

        // PUT api/<PassengerController>/5
        //[HttpPut]
        //public ActionResult<Passenger> Put(string cpf, string newname, string gender, string phone, DateTime dtBirth, string cep, int numero, string complemento)
        //{
        //    Passenger passenger = new()
        //    {
        //        CPF = cpf,
        //        Name = name,
        //        Gender = gender,
        //        Phone = phone,
        //        DtBirth = dtBirth,
        //        DtRegister = DateTime.Now
        //    };
        //    var address = ViaCep.GetAdress(zipcode).Result;
        //    if (address == null) return NotFound();
        //    address.Number = number;
        //    address.Complement = complement;
        //    passenger.Address = address;
        //    return _passengerService.Create(passenger);

        //    var passenger = _passengerService.Get(cpf);
        //    if (passenger == null) return NotFound();
        //    passengerIn.CPF = cpf;
        //    _passengerService.Replace(cpf, passengerIn);
        //    return passenger;
        //}

        // DELETE api/<PassengerController>/5
        [HttpDelete("DeleteByCPF/{cpf}")]
        public ActionResult<Passenger> Delete(string cpf)
        {
            var passenger = _passengerService.Remove(cpf);
            if (passenger == null)
                return NotFound();
            return passenger;
        }
    }
}
