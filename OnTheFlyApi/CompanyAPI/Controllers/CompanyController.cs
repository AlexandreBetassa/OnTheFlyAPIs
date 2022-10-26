using System.Collections.Generic;
using APIViaCep;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companiesService;

        public CompanyController(CompanyService companiesService)
        {
            _companiesService = companiesService;
        }


        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
            var address = ViaCep.GetAdress(company.Address.ZipCode).Result;
            if (address == null) return NotFound();

            company.Address.Street = address.Street;
            company.Address.City = address.City;
            company.Address.State = address.State;
            _companiesService.Create(company);

            return Ok(company);
        }
           
        //[HttpGet("GetOneCep/{cep}")]
        //public ActionResult<Address> GetAddress(string cep, int number, string complemento)
        //{
        //    var address = ViaCep.GetAdress(cep).Result;
        //    address.Number = number;
        //    address.Complement = complemento;
        //    if (address == null) return NotFound();
        //    return Ok(address);

        //}

        [HttpGet]
        public ActionResult<List<Company>> GetAll() => _companiesService.GetAll();

        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<Company> GetOneCNPJ(string cnpj) 
        {
            var companies = _companiesService.GetOneCNPJ(cnpj);
            if(companies == null)return NotFound();

            return Ok(companies);
        }

        [HttpPut]
        public ActionResult<Company>Put(Company companyIn, string cnpj)
        {
            var company = _companiesService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();
            companyIn.CNPJ= cnpj;
            _companiesService.Update(cnpj,companyIn);
            return NoContent();
        }
      

        [HttpDelete("{CNPJ}")]
        public ActionResult<Company> Delete(string cnpj)
        {
            var company = _companiesService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();
            //Fazer um insert na tabela de lixeira
            _companiesService.Delete(company);
            return NoContent();
        }
    }
}
