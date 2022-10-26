using System.Collections.Generic;
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
        public ActionResult<Company> Create(Company company) => _companiesService.Create(company);  

        [HttpGet]
        public ActionResult<List<Company>> GetAll() => _companiesService.GetAll();

        [HttpGet("GetCNPJ/{CNPJ}")]
        public ActionResult<Company> GetOneCNPJ(string cnpj) 
        {
            var companies = _companiesService.GetOneCNPJ(cnpj);
            if(companies == null)return NotFound();

            return companies;
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
