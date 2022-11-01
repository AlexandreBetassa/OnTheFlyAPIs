﻿using APIsConsummers;
using Microsoft.AspNetCore.Mvc;
using Models;
using SaleAPI.Services;
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

        [HttpGet("GetOneSale", Name = "GetOneSale")]
        public ActionResult<Sale> Get(Sale saleIn)
        {
            var sale = _saleService.Get().Where(c => c.Flight.Departure == saleIn.Flight.Departure
            && c.Flight.Plane.RAB == saleIn.Flight.Plane.RAB).FirstOrDefault();
            if (sale == null) return NotFound("Sale not found!!!");
            return Ok(sale);
        }
        #endregion Get

        #region Post
        [HttpPost("CreateSaleByIata")]
        public async Task<ActionResult<AirCraft>> CreateSaleIata(SaleDTO saleDTO)
        {
            //busca o voo para cadastro
            var flight = await FlightAPIConsummer.GetFlight(saleDTO);
            if (flight == null) return NotFound("Flight not found!!!");
            //verifica se há passagem para todos os passageiros da solicitacao de compra
            else if (flight.Sales > saleDTO.PassengersCPFs.Count) return BadRequest("There are no tickets for all passengers");
            var lstPassengers = await PassengersAPIConsummer.GetSalePassengersList(saleDTO.PassengersCPFs, "44355");
            if (lstPassengers == null) return BadRequest("There is a problem with the passengers on the flight");

            Sale sale = new()
            {
                Flight = flight,
                Passenger = lstPassengers,
                Reserved = saleDTO.Reserved,
                Sold = true
            };

            //insere no banco de dados
            sale.Flight.Sales += sale.Passenger.Count;
            if (await FlightAPIConsummer.UpdateFlightSales(sale.Flight))
            {
                _saleService.Create(sale);
                return CreatedAtRoute("GetOneSale", sale, sale);
            }
            else return BadRequest("Unregistered sale");
        }

        #endregion Post

        #region Put
        [HttpPut("PutStatusReserved/{date}/{status}/{aircraft}/{cpf}")]
        public ActionResult<Sale> Put(SaleDTO saleIn/* DateTime date, string aircraft, bool status, string cpf*/)
        {
            //var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date
            //&& saleIn.Flight.Plane.RAB == aircraft && saleIn.Passenger[0].CPF == cpf).FirstOrDefault();
            var sale = _saleService.Get().Where(s => s.Flight.Departure == saleIn.DtFlight && s.Flight.Plane.RAB == saleIn.RAB && s.Flight.Destiny.IATA
            == saleIn.Destiny && s.Passenger[0].CPF == Models.Utils.FormatCPF(saleIn.PassengersCPFs[0])).FirstOrDefault();
            if (sale == null) return BadRequest("\r\nUnable to change. Sale not found");

            sale.Reserved = saleIn.Reserved;
            _saleService.Put(sale);
            return NoContent();
        }
        #endregion Put

    }
}



