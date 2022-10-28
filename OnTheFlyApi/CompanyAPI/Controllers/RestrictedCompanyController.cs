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
        public ActionResult<RestrictedCompany> Create(RestrictedCompany cnpj)
        {
            var restritedCompany = _restritedCompany.GetOneCNPJ(cnpj.CNPJ);
            if (restritedCompany != null) return NoContent() ;

            _restritedCompany.Create(cnpj);
            return Ok(cnpj);
        }

        [HttpGet]
        public ActionResult<List<RestrictedCompany>> GetAll()=> _restritedCompany.GetAll();

        [HttpGet("GetCNPJ/{cnpj}")]
        public ActionResult<RestrictedCompany> GetOneCNPJ(string cnpj)
        {
            var company = _restritedCompany.GetOneCNPJ(cnpj);
            if (company == null) return NotFound();

            return Ok(company);
        }

        [HttpDelete]
        public ActionResult<RestrictedCompany> Delete(string cnpj)
        {
            var restritedCompany = _restritedCompany.GetOneCNPJ(cnpj);
            if(restritedCompany == null) return NotFound();

            _restritedCompany.Delete(restritedCompany);
            return NoContent();
        }
    }
}
