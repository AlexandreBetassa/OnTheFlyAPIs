using APIsConsummers;
using Microsoft.AspNetCore.Mvc;
using Models;
using SaleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;
        public SaleController(SaleService saleService) => _saleService = saleService;

        #region Get
        [HttpGet]
        public ActionResult<List<Sale>> Get() => _saleService.Get();

        [HttpGet("sale", Name = "One")]
        public ActionResult<Sale> Get(Sale saleIn)
        {
            var sale = _saleService.Get().Where(c => c.Flight.Departure == saleIn.Flight.Departure
            && c.Flight.Plane.RAB == saleIn.Flight.Plane.RAB).FirstOrDefault();
            if (sale == null) return NotFound("Sale not found!!!");
            return Ok(sale);
        }
        #endregion Get

        #region Post
        [HttpPost("reserved", Name ="reserved")]
        public async Task<ActionResult<Sale>> Reserved(SaleDTO saleDTO)
        {
            saleDTO.RAB = saleDTO.RAB.ToUpper();
            saleDTO.Destiny = saleDTO.Destiny.ToUpper();

            //search for the flight for registration
            var flight = await FlightAPIConsummer.GetFlight(saleDTO);
            if (flight == null) return NotFound("Flight not found!!!");
            //checks if there is a ticket for all passengers in the purchase request
            else if (flight.Sales > saleDTO.PassengersCPFs.Count) return BadRequest("There are no tickets for all passengers");

            List<Passenger> listPassengers = new();
            foreach (string cpf in saleDTO.PassengersCPFs)
            {
                var passenger = await PassengersAPIConsummer.GetSalePassengersList(cpf.Replace("-", "").Replace(".", ""));
                if (passenger == null) return BadRequest($"CPF not found, {cpf}");
                else if (passenger.Status) return BadRequest($"CPF number {cpf} is on the restricted list.");
                else listPassengers.Add(passenger);
            }

            if ((DateTime.Today - listPassengers[0].DtBirth.AddYears(18)).Days < 0) return BadRequest("Only people over the age of 18 can make sales.");

            Sale sale = new()
            {
                Flight = flight,
                Passenger = listPassengers,
                Reserved = true,
                Sold = false
            };

            _saleService.Create(sale);
            return CreatedAtRoute("One", sale, sale);
        }

        [HttpPost]
        public async Task<ActionResult<AirCraft>> Sale(SaleDTO saleDTO)
        {
            saleDTO.RAB = saleDTO.RAB.ToUpper();
            saleDTO.Destiny = saleDTO.Destiny.ToUpper();

            //search for the flight for registration
            var flight = await FlightAPIConsummer.GetFlight(saleDTO);
            if (flight == null) return NotFound("Flight not found!!!");
            //checks if there is a ticket for all passengers in the purchase request
            else if (flight.Sales > saleDTO.PassengersCPFs.Count) return BadRequest("There are no tickets for all passengers");

            List<Passenger> listPassengers = new();
            foreach (string cpf in saleDTO.PassengersCPFs)
            {
                var passenger = await PassengersAPIConsummer.GetSalePassengersList(cpf.Replace("-", "").Replace(".", ""));
                if (passenger == null) return BadRequest($"CPF not found, {cpf}");
                else if (passenger.Status) return BadRequest($"CPF number {cpf} is on the restricted list.");
                else listPassengers.Add(passenger);
            }

            if ((DateTime.Today - listPassengers[0].DtBirth.AddYears(18)).Days < 0) return BadRequest("Only people over the age of 18 can make sales.");

            Sale sale = new()
            {
                Flight = flight,
                Passenger = listPassengers,
                Reserved = true,
                Sold = true
            };
            _saleService.Create(sale);
            return CreatedAtRoute("One", sale, sale);
        }

        #endregion Post

        #region Put
        [HttpPut]
        public async Task<ActionResult<Sale>> Put(SaleDTO saleIn) //Confirm reservation
        {
            var sale = _saleService.Get().Where(s => s.Flight.Departure.ToString() == saleIn.DtFlight && s.Flight.Plane.RAB == saleIn.RAB && s.Flight.Destiny.IATA
            == saleIn.Destiny && s.Passenger[0].CPF == Models.Utils.FormatCPF(saleIn.PassengersCPFs[0])).FirstOrDefault();
            if (sale == null) return BadRequest("Unable to change. Sale not found");
            if (!sale.Reserved) return BadRequest("Ticket already canceled");
            sale.Sold = true;
            sale.Flight.Sales += sale.Passenger.Count;
            if (await FlightAPIConsummer.UpdateFlightSales(sale.Flight))
            {
                _saleService.Put(sale);
                return CreatedAtRoute("One", sale, sale);
            }
            return BadRequest("Reservation not confirmed");
        }
        #endregion Put

    }
}



