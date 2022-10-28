using System;
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
        public ActionResult<Company> Create(CompanyDTO companyDTO, string rab, int capacity)
        {            
            if (!Utils.ValidateCnpj(companyDTO.CNPJ)) return BadRequest();

            if (_companyService.GetOneCNPJ(companyDTO.CNPJ) != null) return BadRequest();

            var unformattedCNPJ = companyDTO.CNPJ;
            companyDTO.CNPJ = Utils.FormatCNPJ(unformattedCNPJ);

            if (companyDTO.NameOp == null) companyDTO.NameOp = companyDTO.Name;

            var address = ViaCepAPIConsummer.GetAdress(companyDTO.Address.ZipCode).Result;
            if (address == null) return NotFound();

            Company company = new()
            {
                CNPJ = companyDTO.CNPJ,
                Name = companyDTO.Name.ToUpper(),
                NameOp = companyDTO.NameOp.ToUpper(),
                DtOpen = DateTime.Now,
                Status = false,
                Address = new Address
                {
                    ZipCode = address.ZipCode,
                    Street = address.Street,
                    Number = companyDTO.Address.Number,
                    Complement = companyDTO.Address.Complement.ToUpper(),
                    City = address.City.ToUpper(),
                    State = address.State.ToUpper()
                }
            };
           
            if (_restrictedCompanyService.GetOneCNPJ(companyDTO.CNPJ) != null) company.Status= true;

            _companyService.Create(company);

            AirCraft airCraft = new AirCraft
            {
                Capacity = capacity,
                RAB = rab,
                DtRegistry = DateTime.Now,
                DtLastFlight = DateTime.Now,
                Company = company
            };

           var savedAirCraft = AirCraftAPIConsummer.PostAirCraft(airCraft).Result;
          
            if(savedAirCraft) company.Status = true;

            return Ok(company);
        }

        #region GETs

        [HttpGet]
        public ActionResult<List<Company>> GetAll() => _companyService.GetAll();

        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<Company> GetOneCNPJ(string cnpj) 
        {
            var company = _companyService.GetOneCNPJ(cnpj);
            if(company == null)return NotFound();

            return Ok(company);
        }

        #endregion

        #region PUTs

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

        #endregion

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
