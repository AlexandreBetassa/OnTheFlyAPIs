using Microsoft.AspNetCore.Mvc;
using Models;
using PassengerAPI.Services;
using System.Collections.Generic;
using System;
using APIsConsummers;
using System.Linq;

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
            return Ok(_passengerService.Get());
        }

        // GET api/<PassengerController>/5
        [HttpGet("GetByCPF/{unformattedCpf}")]
        public ActionResult<Passenger> Get(string unformattedCpf)
        {
            var passenger = _passengerService.Get(Models.Utils.FormatCPF(unformattedCpf));
            if (passenger == null) return NotFound();
            return Ok(passenger);
        }

        [HttpPost("GetSalePassengersList")]
        public ActionResult<List<Passenger>> GetSalePassengersList(List<string> unformattedCpfList)
        {
            Passenger passenger;
            List<Passenger> passengersList = new();

            if (unformattedCpfList.Count == 0) return BadRequest();

            if (!unformattedCpfList.GroupBy(c => c).All(c => c.Count() == 1)) return BadRequest();

            foreach (string cpf in unformattedCpfList)
            {
                passenger = _passengerService.Get(Models.Utils.FormatCPF(cpf));
                if (passenger == null) return NotFound();
                if (passenger.Status == true) return BadRequest("Unauthorized passenger on the list");
                passengersList.Add(passenger);
            }

            if ((DateTime.Today - passengersList[0].DtBirth.AddYears(18)).Days < 0)
                return BadRequest();

            return passengersList;
        }

        // POST api/<PassengerController>
        [HttpPost("Create")]
        public ActionResult<Passenger> Post(PassengerDTO p)
        {
            if (!Models.Utils.CPFIsValid(p.UnformattedCPF)) return BadRequest();

            string formattedCpf = Models.Utils.FormatCPF(p.UnformattedCPF);

            if (_passengerService.Get(formattedCpf) != null) return Unauthorized();

            var address = ViaCepAPIConsummer.GetAdress(p.Address.ZipCode).Result;
            if (address == null) return NotFound();

            Passenger passenger = new()
            {
                CPF = formattedCpf,
                Name = p.Name.ToUpper(),
                Gender = p.Gender.ToUpper(),
                Phone = p.PhoneOpt,
                DtBirth = p.DtBirth,
                DtRegister = DateTime.Now,
                Status = false,
                Address = new Address
                {
                    ZipCode = address.ZipCode,
                    Street = address.Street.ToUpper(),
                    Number = p.Address.Number,
                    Complement = p.Address.Complement.ToUpper(),
                    City = address.City.ToUpper(),
                    State = address.State.ToUpper()
                }
            };

            if (_restrictedPassengerService.Get(formattedCpf) != null) passenger.Status = true;

            return Ok(_passengerService.Create(passenger));
        }

        // PUT api/<PassengerController>/5
        [HttpPut("Update")]
        public ActionResult<Passenger> Put(PassengerUpdateDTO p)
        {
            if (!Models.Utils.CPFIsValid(p.UnformattedCPF)) return BadRequest();

            string formattedCpf = Models.Utils.FormatCPF(p.UnformattedCPF);

            var passenger = _passengerService.Get(formattedCpf);
            if (passenger == null) return BadRequest();

            var address = ViaCepAPIConsummer.GetAdress(p.NewAddress.ZipCode).Result;
            if (address == null) return NotFound();

            passenger.Name = p.NewName.ToUpper();
            passenger.Gender = p.NewGender.ToUpper();
            passenger.Phone = p.NewPhoneOpt;
            passenger.Address = new Address
            {
                ZipCode = address.ZipCode,
                Street = address.Street.ToUpper(),
                Number = p.NewAddress.Number,
                Complement = p.NewAddress.Complement.ToUpper(),
                City = address.City.ToUpper(),
                State = address.State.ToUpper()
            };

            _passengerService.Replace(formattedCpf, passenger);
            return Ok(passenger);
        }

        // DELETE api/<PassengerController>/5
        [HttpDelete("DeleteByCPF/{unformattedCpf}")]
        public ActionResult<Passenger> Delete(string unformattedCpf)
        {
            var passenger = _passengerService.Remove(Models.Utils.FormatCPF(unformattedCpf));
            if (passenger == null) return NotFound();
            return Ok(passenger);
        }
    }
}
