﻿using System;
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
            if (!Utils.ValidateCnpj(companyDTO.CNPJ)) return BadRequest("Invalid CNPJ");
                       
            var unformattedCNPJ = companyDTO.CNPJ;
            companyDTO.CNPJ = Utils.FormatCNPJ(unformattedCNPJ);

            if (_companyService.GetOneCNPJ(companyDTO.CNPJ) != null) return BadRequest("This CNPJ is already registered");

            if ((companyDTO.NameOp == null) || (companyDTO.NameOp == "string")) companyDTO.NameOp = companyDTO.Name;

            var address = ViaCepAPIConsummer.GetAdress(companyDTO.Address.ZipCode).Result;
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
                    Street = address.Street,
                    Number = companyDTO.Address.Number,
                    Complement = companyDTO.Address.Complement.ToUpper(),
                    City = address.City.ToUpper(),
                    State = address.State.ToUpper()
                }
            };

            if (_restrictedCompanyService.GetOneCNPJ(companyDTO.CNPJ) != null) company.Status = true;

            _companyService.Create(company);

            AirCraftDTO airCraft = new AirCraftDTO
            {
                Capacity = capacity,
                RAB = rab,
                CompanyCnpj = unformattedCNPJ
            };

            var savedAirCraft = AirCraftAPIConsummer.PostAirCraft(airCraft).Result;

            if (!savedAirCraft) company.Status = true;

            return Ok(company);
        }

        #region GETs

        [HttpGet]
        public ActionResult<List<Company>> GetAll() => _companyService.GetAll();

        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<Company> GetOneCNPJ(string cnpj)
        {
            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            return Ok(company);
        }

        #endregion

        #region PUTs

        [HttpPut("PutNameOP/{newNameOp}")]
        public ActionResult<Company> PutNameOp(string cnpj, string newNameOp)
        {

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.NameOp = newNameOp;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }
        [HttpPut("PutStatus/{newStatus}")]
        public ActionResult<Company> PutStatus(string cnpj, bool newStatus)
        {

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            company.Status = newStatus;
            _companyService.Update(cnpj, company);

            return Ok(company);
        }

        [HttpPut("PutCEP/{newCEP}")]
        public ActionResult<Company> PutCep(string cnpj, string newCEP)
        {

            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            var address = ViaCepAPIConsummer.GetAdress(newCEP).Result;
            if (address == null) return NotFound();

            company.Address.ZipCode = address.ZipCode;
            company.Address.Street = address.Street;
            company.Address.City = address.City;
            company.Address.State = address.State;

            return Ok(company);
        }

        [HttpPut("PutStreet/{newStreet}")]
        public ActionResult<Company> PutStreet(string cnpj, string newStreet)
        {
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
