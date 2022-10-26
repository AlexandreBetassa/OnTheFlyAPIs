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

        [HttpGet("GetByDate/{date:length(10)}", Name = "GetSale")]
        public ActionResult<Sale> Get(DateTime date)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date);
            return Ok(sale);
        }

        [HttpPost("CreateSale")]
        public ActionResult<AirCraft> Create(Sale sale)
        {
            _saleService.Create(sale);
            return CreatedAtRoute("GetSale", sale.Flight.Departure);
        }

        [HttpPut("{date:length(10)}")]
        public ActionResult<Sale> Put(DateTime date, string aircraft)
        {
            _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date && saleIn.Flight.Plane.RAB == aircraft);
            return Ok();
        }

    }
}
