using FlightsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using APIsConsummers;
using System.Linq;
using System.Threading.Tasks;

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

            if (flight == null) return NotFound();

            return Ok(flight);
        }

        [HttpPost("GetOne/", Name = "GetOne")]
        public ActionResult<Flight> Get([FromBody] SaleDTO sale)
        {
            var flight = _flightService.GetOne(sale.DtFlight, sale.RAB.ToUpper(), sale.Destiny.ToUpper());

            if (flight == null) return NotFound();

            return Ok(flight);
        }

        [HttpPost("{rab}/{destiny}/{dateFlight}")]
        public async Task<ActionResult<Flight>> Create(string rab, DateTime dateFlight, string destiny)
        {
            AirCraft airCraft = await AirCraftAPIConsummer.GetAirCraft(rab.ToUpper());
            if (airCraft == null) return NotFound();
            if (airCraft.Company.Status == true) return BadRequest("Restricted Airline, flights can only be registered for unrestricted airlines.");

            Airport airport = new Airport { Country = "BR", /*State = "SP"*/ IATA = destiny.ToUpper() }; /*Consumo api pestana*/
            if (airport == null) return NotFound();

            if (dateFlight < DateTime.Now) return BadRequest("Invalid Date, the date must be a future date the current date.");

            Flight flight = new() { Plane = airCraft, Departure = dateFlight, Destiny = airport, Sales = 0, Status = true };

            var flightList = _flightService.Get();
            var flighOfListWithEqualInformation = flightList.FirstOrDefault(f => f.Plane.RAB == flight.Plane.RAB && f.Departure == dateFlight);

            if (flighOfListWithEqualInformation != null)
                if (flighOfListWithEqualInformation.Plane.RAB == flight.Plane.RAB && flighOfListWithEqualInformation.Departure == flight.Departure) return BadRequest("This flight information already exists, cannot register more than one flight with the same plane on the same date.");

            _flightService.Create(flight);

            airCraft.DtLastFlight = dateFlight;
            var lastPlaneFlight = AirCraftAPIConsummer.UpdateAirCraft(airCraft);

            return Ok(flight);
        }

        [HttpPut("ModifyFlightSales", Name = "ModifyFlightSales")]
        public ActionResult<Flight> UpdateSalesFlight([FromBody]Flight flight)
        {
            var flightUpdate = _flightService.GetOne(flight.Departure, flight.Plane.RAB.ToUpper(), flight.Destiny.IATA.ToUpper());

            if (flightUpdate == null) return NotFound();

            _flightService.UpdateSales(flight.Departure, flight.Plane.RAB, flight.Destiny.IATA, flightUpdate);

            return NoContent();
        }

        [HttpPut("ModifyFlightStatus/{fullDate}/{rabPlane}/{destiny}/{newStatus}", Name = "ModifyFlightStatus")]
        public ActionResult<Flight> UpdateStatus(DateTime fullDate, string rabPlane, string destiny, bool newStatus)
        {
            var flightUpdate = _flightService.GetOne(fullDate, rabPlane.ToUpper(), destiny.ToUpper());
            if (flightUpdate == null) return NotFound();

            flightUpdate.Status = newStatus;

            _flightService.UpdateStatus(fullDate, rabPlane, destiny, flightUpdate);

            return NoContent();
        }
    }
}
