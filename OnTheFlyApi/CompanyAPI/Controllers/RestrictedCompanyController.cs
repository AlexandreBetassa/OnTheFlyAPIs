using System.Collections.Generic;
using CompanyAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace CompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictedCompanyController : ControllerBase
    {
        private readonly RestrictedCompanyService _restritedCompany;
        private readonly CompanyService _companyService;

        public RestrictedCompanyController(RestrictedCompanyService restritedCompany, CompanyService companyService)
        {
            _restritedCompany = restritedCompany;
            _companyService = companyService;
        }
        [HttpPost]
        public ActionResult<RestrictedCompany> Create(string cnpj)
        {
            RestrictedCompany restrictedCompany = new()
            {
                CNPJ = cnpj,
            };
            string unformattedCNPJ = restrictedCompany.CNPJ;
            restrictedCompany.CNPJ = Utils.FormatCNPJ(unformattedCNPJ);

            var restritedCnpj = _restritedCompany.GetOneCNPJ(restrictedCompany.CNPJ);
            if (restritedCnpj != null) return BadRequest() ;

          var company = _companyService.GetOneCNPJ(restrictedCompany.CNPJ);
            if (company != null)
            {
                company.Status = true;
                _companyService.Update(company.CNPJ, company);
            }
        
            return Ok(_restritedCompany.Create(restrictedCompany));
        }

        [HttpGet]
        public ActionResult<List<RestrictedCompany>> GetAll()=> _restritedCompany.GetAll();

        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<RestrictedCompany> GetOneCNPJ(string cnpj)
        {
            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var company = _restritedCompany.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            return Ok(company);
        }

        [HttpDelete]
        public ActionResult<RestrictedCompany> Delete(string cnpj)
        {
            var unformattedCNPJ = cnpj;
            cnpj = Utils.FormatCNPJ(unformattedCNPJ);

            var restritedCompany = _restritedCompany.GetOneCNPJ(cnpj);
            if(restritedCompany == null) return NotFound();

            var company = _companyService.GetOneCNPJ(cnpj);
            if (company != null)
            {
                company.Status = false;
                _companyService.Update(cnpj, company);
            }

            _restritedCompany.Delete(restritedCompany);
            return NoContent();
        }
    }
}
