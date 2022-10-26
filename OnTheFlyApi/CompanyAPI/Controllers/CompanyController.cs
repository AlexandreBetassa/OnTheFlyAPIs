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
        private readonly CompanyService _companyService;
        private readonly CompanyService _deletedcompanyService;

        public CompanyController(CompanyService companiesService)
        {
            _companyService = companiesService;
        }


        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
            var address = ViaCep.GetAdress(company.Address.ZipCode).Result;
            if (address == null) return NotFound();

            company.Address.Street = address.Street;
            company.Address.City = address.City;
            company.Address.State = address.State;
            _companyService.Create(company);

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
        public ActionResult<List<Company>> GetAll() => _companyService.GetAll();

        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<Company> GetOneCNPJ(string cnpj) 
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if(company == null)return NotFound();

            return Ok(company);
        }

        [HttpPut("PutNameOP/{newNameOP}")]
        public ActionResult<Company>PutNameOp(string cnpj, string newNameOp)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.NameOp= newNameOp;
            _companyService.Update(cnpj,company);

            return Ok(company);
        }
        [HttpPut("PutStatus/{newStatus}")]
        public ActionResult<Company> PutStatus(string cnpj, bool newStatus)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Status = newStatus;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }

        [HttpPut("PutStreet/{newStreet}")]
        public ActionResult<Company> PutStreet(string cnpj, string newStreet)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Address.Street = newStreet;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }

        [HttpPut("PutNumber/{newNumber}")]
        public ActionResult<Company> PutNumber(string cnpj, int newNumber)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Address.Number = newNumber;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }
        [HttpPut("PutComplement/{newComplement}")]
        public ActionResult<Company> PutComplement(string cnpj, string newComplement)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Address.Complement = newComplement;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }

        [HttpDelete("{cnpj}")]
        public ActionResult<Company> Delete(string cnpj)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            _deletedcompanyService.Create(company);
            _companyService.Delete(company);

            return NoContent();
        }
    }
}
