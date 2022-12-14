using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<ActionResult<Company>> Create(CompanyDTO companyDTO, string rab, int capacity)
        {
          
            if (!Utils.ValidateCnpj(companyDTO.CNPJ)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");
                       
            var unformattedCNPJ = companyDTO.CNPJ;
            companyDTO.CNPJ = Utils.FormatCNPJ(unformattedCNPJ);

            if (_companyService.GetOneCNPJ(companyDTO.CNPJ) != null) return BadRequest("This CNPJ is already registered");

            AddressDTOViaCep address = await ViaCepAPIConsummer.GetAdress(companyDTO.Address.ZipCode);
            if (address == null) return NotFound();

            Company company = new()
            {
                CNPJ = companyDTO.CNPJ,
                Name = companyDTO.Name.ToUpper(),
                NameOp = companyDTO.NameOp.ToUpper(),
                DtOpen = companyDTO.DtOpen,
                Status = false,
                Address = new Address
                {
                    ZipCode = address.ZipCode,
                    Street = address.Street.ToUpper(),
                    Number = companyDTO.Address.Number,
                    Complement = companyDTO.Address.Complement.ToUpper(),
                    City = address.City.ToUpper(),
                    State = address.State.ToUpper()
                }
            };

            if (!Utils.ValidateCompanyTime(company))return BadRequest("You cannot register companies that are less than 6 months old");

            if (_restrictedCompanyService.GetOneCNPJ(companyDTO.CNPJ) != null) company.Status = true;

            if ((company.NameOp == "STRING")|| (company.NameOp == null)) company.NameOp = company.Name;
           
            _companyService.Create(company);

            AirCraft airCraft = new ()
            {
                Capacity = capacity,
                RAB = rab,
                Company = company,
                DtLastFlight = DateTime.Now,
                DtRegistry = DateTime.Now
            };

           
            var savedAirCraft = AirCraftAPIConsummer.PostAirCraft(airCraft).Result;

            if (!savedAirCraft)
            {
                Delete(company.CNPJ.Replace(".","").Replace("/","").Replace("-",""));
                return BadRequest("An error occurred in the aircraft registration");
            }
           
            return Ok(company);
        }




        #region GETs
        [HttpGet]
        public ActionResult<List<Company>> GetAll() => _companyService.GetAll();


        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<Company> GetOneCNPJ(string cnpj)
        {
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            return Ok(company);
        }
        #endregion



        #region PUTs
        [HttpPut("PutNameOP/")]
        public ActionResult<Company> PutNameOp(string cnpj, string newNameOp)
        {
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();


            if (newNameOp == null) company.NameOp = company.Name;
            else
                company.NameOp = newNameOp;            
            
                _companyService.Update(cnpj, company);

            return Ok(company);
        }



        [HttpPut("PutCEP/{newCEP}")]
        public ActionResult<Company> PutCep(string cnpj, string newCEP)
        {
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            if (!long.TryParse(newCEP, out long aux)) return BadRequest("This zip code is invalid, check that it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            var address = ViaCepAPIConsummer.GetAdress(newCEP).Result;
            if (address.ZipCode == null) return NotFound();

            company.Address.ZipCode = address.ZipCode;
            company.Address.Street = address.Street;
            company.Address.City = address.City;
            company.Address.State = address.State;

            return Ok(company);
        }


        [HttpPut("PutStreet/{newStreet}")]
        public ActionResult<Company> PutStreet(string cnpj, string newStreet)
        {
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Address.Street = newStreet;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }


        [HttpPut("PutNumber/{newNumber}")]
        public ActionResult<Company> PutNumber(string cnpj, int newNumber)
        {
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Address.Number = newNumber;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }


        [HttpPut("PutComplement/{newComplement}")]
        public ActionResult<Company> PutComplement(string cnpj, string newComplement)
        {
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

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
            if (!Utils.ValidateCnpj(cnpj)) return BadRequest("This CNPJ is invalid, check if it contains only numbers");

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            _deletedCompanyService.Insert(company);
            _companyService.Delete(company);

            return NoContent();
        }
    }
}
