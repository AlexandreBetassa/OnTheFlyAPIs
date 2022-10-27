using APIsConsummers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        public SaleController(SaleService saleService) => _saleService = saleService;

        [HttpGet]
        public ActionResult<List<Sale>> Get() => _saleService.Get();

        [HttpGet("GetSale/{date}", Name = "GetSale")]
        public ActionResult<Sale> Get(DateTime date)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date).FirstOrDefault();
            if (sale == null) return NotFound("Venda não localizada");
            return Ok(sale);
        }

        [HttpPost("CreateSale")]
        public ActionResult<AirCraft> Create(Sale sale)
        {
            _saleService.Create(sale);
            return CreatedAtRoute("GetSale", new { date = sale.Flight.Departure.ToString() }, sale);
        }

        [HttpPut("{date},{status},{aircraft}")]
        public ActionResult<Sale> Put(DateTime date, string aircraft, bool status)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date && saleIn.Flight.Plane.RAB == aircraft).FirstOrDefault();
            if (sale == null) return BadRequest("Impossível alterar. Venda não localizada");
            sale.Reserved = status;
            _saleService.Put(sale);
            return NoContent();
        }
    }
}
