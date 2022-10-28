using FlightsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using APIsConsummers;

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

        [HttpGet("GetFlightByDate/{date}", Name = "GetFlightByDate")]
        public ActionResult<Flight> Get(DateTime date)
        {
            var flight = _flightService.GetByDate(date);

            if (flight == null)
                return NotFound();

            return Ok(flight); 
        }

        [HttpGet("GetOne/{fullDate}/{rabPlane}/{destiny}", Name = "GetOne")]
        public ActionResult<Flight> Get(DateTime fullDate, string rabPlane, string destiny)
        {
            var flight = _flightService.GetOne(fullDate, rabPlane, destiny);
        
            if (flight == null)
                return NotFound();
        
            return Ok(flight);
        }

        //verificar a existencia de companhia, aeronave e aeroporto
        //não pode cadastrar voos para uma companhia que esteja bloqueada (fazer tal verificação)
        [HttpPost]
        public ActionResult<Flight> Create(Flight flight)
        {
            var airCraft = AirCraftAPIConsummer.GetAirCraft(flight.Plane.RAB).Result;
            if (airCraft == null)
                return NotFound();

            flight.Plane = airCraft;

            _flightService.Create(flight);
            //return CreatedAtRoute("GetOne", new { date = flight.Departure }, flight);
            return Ok(flight);
        }

        [HttpPut("ModifyFlightSales", Name = "ModifyFlightSales")]
        public ActionResult<Flight> UpdateSalesFlight(Flight flight)
        {
            var flightUpdate = _flightService.GetOne(flight.Departure, flight.Plane.RAB, flight.Destiny.IATA);

            if (flightUpdate == null)
                return NotFound();

            _flightService.UpdateSales(flight.Departure, flight.Plane.RAB, flight.Destiny.IATA, flightUpdate);

            return NoContent();
        }

        [HttpPut("ModifyFlightStatus/{fullDate}/{rabPlane}/{destiny}/{newStatus}", Name = "ModifyFlightStatus")]
        public ActionResult<Flight> UpdateStatus(DateTime fullDate, string rabPlane, string destiny, bool newStatus)
        {
            var flightUpdate = _flightService.GetOne(fullDate, rabPlane, destiny);
            if (flightUpdate == null)
                return NotFound();

            flightUpdate.Status = newStatus;

            _flightService.UpdateStatus(fullDate, rabPlane, destiny, flightUpdate);

            return NoContent();
        }
    }
}
