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

        [HttpGet("GetByDate/{date:length(10)}")]
        public ActionResult<Sale> Get(DateTime date)
        {
            var sale = _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date);
            return Ok(sale);
        }
        [HttpPost]
        public ActionResult<Sale> Create(Sale sale, string cnpj)
        {
            _saleService.Create(sale);
            Company c = new Company();
            var p = CreatedAtRoute("localhost: 44355", c);
            return Ok(sale);
        }

        [HttpPut("{date:length(10)}")]
        public ActionResult<Sale> Put(DateTime date, string aircraft)
        {
            _saleService.Get().Where(saleIn => saleIn.Flight.Departure == date && saleIn.Flight.Plane.RAB == aircraft);
            return Ok();
        }
    }
}
