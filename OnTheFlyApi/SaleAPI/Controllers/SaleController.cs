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

        [HttpGet]
        public ActionResult<List<Sale>> Get() => _saleService.Get();

        [HttpGet("GetSale/{date}/{aircraft}", Name = "GetSale")]
        public ActionResult<Sale> Get(DateTime date, string aircraft)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date && saleIn.Flight.Plane.RAB == aircraft).FirstOrDefault();
            if (sale == null) return NotFound("Venda não localizada");
            return Ok(sale);
        }

        [HttpPost("CreateSale")]
        public async Task<ActionResult<AirCraft>> Create(SaleDTO saleDTO)
        {
            //busca o voo para cadastroartur
            var flight = await FlightAPIConsummer.GetFlight(saleDTO.Flight.Departure, saleDTO.Flight.Plane.RAB, saleDTO.Flight.Destiny.IATA);
            if (flight == null) return NotFound("Voo não localizado!!!");
            //verifica se há passagem para todos os passageiros da solicitacao de compra
            else if (flight.Sales <= saleDTO.PassengersCPFs.Count) return BadRequest("Não há passagens para todos os passageiros");
            var lstPassengers = await PassengersAPIConsummer.PostListPassengers(saleDTO.PassengersCPFs);
            if (lstPassengers == null) return BadRequest("Há um problema com os passegeiros do voo");

            Sale sale = new()
            {
                Flight = flight,
                Passenger = lstPassengers,
                Reserved = saleDTO.Reserved,
                Sold = true
            };

            //insere no banco de dados
            _saleService.Create(sale);
            await FlightAPIConsummer.UpdateFlightSales(sale.Flight);
            return CreatedAtRoute("GetSale", new { date = saleDTO.Flight.Departure.ToString(), rab = saleDTO.Flight.Plane.RAB.ToString(), sale });
        }

        [HttpPut("PutStatusReserved/{date}/{status}/{aircraft}/{cpf}")]
        public ActionResult<Sale> Put(DateTime date, string aircraft, bool status, string cpf)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date
            && saleIn.Flight.Plane.RAB == aircraft && saleIn.Passenger[0].CPF == cpf).FirstOrDefault();
            if (sale == null) return BadRequest("Impossível alterar. Venda não localizada");
            //
            /* acionar endpoint de get de passageiros restritos (endpoint dany)*/
            //
            sale.Reserved = status;
            _saleService.Put(sale);
            return NoContent();
        }

        //confirmar com pestana

        //[HttpPut("PutStatusCancelFlight")]
        //public ActionResult<Sale> Put(Flight flight)
        //{
        //    var saleDTO = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == flight.Departure
        //    && saleIn.Flight.Plane.RAB == flight.Plane.RAB).FirstOrDefault();
        //    if (saleDTO == null) return BadRequest("Impossível alterar. Venda não localizada");
        //    //
        //    /* acionar endpoint de get de passageiros restritos (endpoint dany)*/
        //    //
        //    saleDTO.Reserved = status;
        //    _saleService.Put(saleDTO);
        //    return NoContent();
        //}
    }
}
