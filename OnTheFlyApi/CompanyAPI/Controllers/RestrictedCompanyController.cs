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

        public RestrictedCompanyController(RestrictedCompanyService restritedCompany)
        {
            _restritedCompany = restritedCompany;
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

            _restritedCompany.Create(restrictedCompany);
            
            CreatedAtRoute("Status", new { cnpj,status = true});



            return Ok(restrictedCompany);
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

            _restritedCompany.Delete(restritedCompany);
            return NoContent();
        }
    }
}
