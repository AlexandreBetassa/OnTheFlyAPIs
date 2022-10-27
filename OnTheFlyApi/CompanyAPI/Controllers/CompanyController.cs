using System.Collections.Generic;
using APIsConsummers;
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
        private readonly CompanyService _deletedCompanyService;
        private readonly RestrictedCompanyService _restrictedCompanyService;

        public CompanyController(CompanyService companyService, CompanyService deletedCompanyService, RestrictedCompanyService restrictedCompanyService)
        {
            _companyService = companyService;
            _deletedCompanyService = deletedCompanyService;
            _restrictedCompanyService = restrictedCompanyService;
        }   

        [HttpPost]
        public ActionResult<Company> Create(Company company)
        {
          
            if (_companyService.GetOneCNPJ(company.CNPJ)!=null) return BadRequest();

            if(_restrictedCompanyService.GetOneCNPJ(company.CNPJ)!= null) company.Status = true;
       
            var address = ViaCepAPIConsummer.GetAdress(company.Address.ZipCode).Result;
            if (address == null) return NotFound();

            if (company.NameOp == null) company.NameOp = company.Name;

            company.Address.Street = address.Street;
            company.Address.City = address.City;
            company.Address.State = address.State;
     
            _companyService.Create(company);

            return Ok(company);
        }
        

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

        [HttpPut("PutCEP/{newCEP}")]
        public ActionResult<Company> PutCep(string cnpj, string newCEP)
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            var address = ViaCepAPIConsummer.GetAdress(newCEP).Result;
            if (address == null) return NotFound();

            company.Address.Street = address.Street;
            company.Address.City = address.City;
            company.Address.State = address.State;

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

            _deletedCompanyService.Insert(company);
            _companyService.Delete(company);

            return NoContent();
        }
    }
}
