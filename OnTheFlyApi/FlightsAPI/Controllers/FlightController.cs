using FlightsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;

namespace FlightsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightServices _flightService;

        public FlightController(FlightServices flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult<List<Flight>> Get() => _flightService.Get();

        [HttpGet(Name = "GetFlightByDate")]
        public ActionResult<Flight> Get(DateTime date)
        {
            var flight = _flightService.GetByDate(date);

            if (flight == null)
                return NotFound();

            return Ok(flight); 
        }

        //[HttpGet(Name = "GetOneFlight")] // get pela hora
        //public ActionResult<Flight> Get(DateTime fullDate)
        //{
        //    var flight = _flightService.GetOne(fullDate);

        //    if (flight == null)
        //        return NotFound();

        //    return Ok(flight);
        //}


        //não pode cadastrar voos para uma companhia que esteja bloqueada (fazer tal verificação)
        [HttpPost]
        public ActionResult<Flight> Create(Flight flight)
        {
            _flightService.Create(flight);
            return CreatedAtRoute("GetFlightByDate", new { date = flight.Departure }, flight);
        }

        [HttpPut]
        public ActionResult<Flight> Update(Flight flightIn, DateTime date)
        {
            var flight = _flightService.GetByDate(date);

            if (flight == null)
                return NotFound();

            _flightService.Update(date, flightIn);

            return NoContent();
        }
    }
}
