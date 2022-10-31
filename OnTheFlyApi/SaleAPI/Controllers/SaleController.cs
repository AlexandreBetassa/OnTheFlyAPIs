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

        [HttpGet("GetOneSale", Name = "GetOneSale")]
        public ActionResult<Sale> Get(Sale saleIn)
        {
            var sale = _saleService.Get().Where(c => c.Flight.Departure == saleIn.Flight.Departure
            && c.Flight.Plane.RAB == saleIn.Flight.Plane.RAB).FirstOrDefault();
            if (sale == null) return NotFound("Sale not found!!!");
            return Ok(sale);
        }

        [HttpPost("CreateSale")]
        public async Task<ActionResult<AirCraft>> Create(SaleDTO saleDTO)
        {
            //busca o voo para cadastroartur
            var flight = await FlightAPIConsummer.GetFlight(saleDTO);
            if (flight == null) return NotFound("Flight not found!!!");
            //verifica se há passagem para todos os passageiros da solicitacao de compra
            else if (flight.Sales > saleDTO.PassengersCPFs.Count) return BadRequest("\r\nThere are no tickets for all passengers");
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
            _saleService.Create(sale);
            sale.Flight.Sales += sale.Passenger.Count;
            if (await FlightAPIConsummer.UpdateFlightSales(sale.Flight)) return CreatedAtRoute("GetOneSale", sale, sale);
            else return BadRequest();
        }


        }
        [HttpPut("PutStatusReserved/{date}/{status}/{aircraft}/{cpf}")]
        public ActionResult<Sale> Put(DateTime date, string aircraft, bool status, string cpf)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date
            && saleIn.Flight.Plane.RAB == aircraft && saleIn.Passenger[0].CPF == cpf).FirstOrDefault();
            if (sale == null) return BadRequest("Impossível alterar. Venda não localizada");

            sale.Reserved = status;
            _saleService.Put(sale);
            return NoContent();
        }
    }

}


