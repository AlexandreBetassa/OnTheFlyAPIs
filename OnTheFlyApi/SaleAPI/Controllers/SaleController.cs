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
    [Route("api/sales")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;
        public SaleController(SaleService saleService) => _saleService = saleService;

        #region Get
        [HttpGet]
        public ActionResult<List<Sale>> Get() => _saleService.Get();

        [HttpGet("sale", Name = "one")]
        public ActionResult<Sale> Get(string date, string rab, string cpf, string destination)
        {
            var sale = _saleService.Get()
                .Where(c => c.Flight.Departure == DateTime.Parse(date)
                 && c.Flight.Plane.RAB == rab
                 && c.Passenger[0].CPF == cpf
                 && c.Flight.Destiny.IATA == destination)
                .FirstOrDefault();
            if (sale == null) return NotFound("Sale not found!!!");
            return Ok(sale);
        }
        #endregion Get

        #region Post
        [HttpPost("reserved", Name = "reserved")]
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
            return CreatedAtRoute("one", new
            {
                date = sale.Flight.Departure,
                cpf = sale.Passenger[0].CPF,
                rab = sale.Flight.Plane.RAB,
                destination = sale.Flight.Destiny.IATA
            }, sale);
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
            return CreatedAtRoute("one", saleDTO, sale);
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
                return CreatedAtRoute("one", sale, sale);
            }
            return BadRequest("Reservation not confirmed");
        }
        #endregion Put

    }
}



