using APIsConsummers;
using Microsoft.AspNetCore.Mvc;
using Models;
using SaleAPI.RabbitMQ;
using SaleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SaleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;
        private readonly SendRabbitMQ _sendRabbitMQ;
        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
            _sendRabbitMQ = new SendRabbitMQ();
        }

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
        public ActionResult<AirCraft> Create(Sale sale)
        {
            int count = 0;
            _sendRabbitMQ.Send(sale);
            //busca o voo para cadastro
            var flight = FlightAPIConsummer.GetFlight(sale.Flight.Departure, sale.Flight.Plane.RAB, sale.Flight.Destiny.IATA).Result;
            if (flight == null) return NotFound("Voo não localizado!!!");
            //verifica se há passagem para todos os passageiros da solicitacao de compra
            else if (flight.Sales <= sale.Passenger.Count) return BadRequest("Não há passagens para todos os passageiros");
            //verifica se menores de 18 anos está tentndo comprar passagens
            else if ((DateTime.Now - sale.Passenger[0].DtBirth).TotalDays / 365 < 18) return BadRequest("Passegeiros menores de idade não podem efetuar compras");
            //verifica se há passageiros com restrições
            else foreach (var passenger in sale.Passenger) if (passenger.Status == true) return BadRequest("Existe passegeiro impedido de viajar incluso na solicitação");
            //verifica se todos os passageiros da passagem estão cadastrados no banco de dados do aeroporto
            var lstPassenger = PassengersAPIConsummer.GetPassengers().Result;
            foreach (var passenger in lstPassenger)
                foreach (var passengerIndex in sale.Passenger) if (passenger.CPF == passengerIndex.CPF) count += 1;
            if (count != sale.Passenger.Count) return BadRequest("CPF da venda não localizado no banco de dados do aeroporto");
            //insere no banco de dados
            _saleService.Create(sale);
            if (CreatedAtRoute("GetSale", new { date = sale.Flight.Departure.ToString(), rab = sale.Flight.Plane.RAB.ToString() }, sale).StatusCode == 200) { /*api para editar quantidades de vendas do voo;*/}
            /*metodo put para atualizar quantidade de passagens vendidas do voo (endpoint artur)*/
            return Ok();
        }

        [HttpPut("{date},{status},{aircraft},{cpf}")]
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
    }
}
