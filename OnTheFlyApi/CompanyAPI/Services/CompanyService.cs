using System.Collections.Generic;
using CompanyAPI.DatabaseSettings;
using Models;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _company;
        private readonly IMongoCollection<Company> _deletedCompany;

        public CompanyService(IDatabaseSettings settings)
        {
            var company = new MongoClient(settings.ConnectionString);
            var database = company.GetDatabase(settings.DatabaseName);
            _company = database.GetCollection<Company>(settings.CompanyCollectionName);
            _deletedCompany = database.GetCollection<Company>(settings.DeletedCompanyCollectionName);
        }

        public Company Create(Company company){ _company.InsertOne(company); return company; }
        public List<Company> GetAll() => _company.Find<Company>(company => true).ToList();
        public Company GetOneCNPJ(string cnpj) => _company.Find<Company>(company => company.CNPJ == cnpj).FirstOrDefault();
        public void Update(string cnpj, Company CompanyIn) => _company.ReplaceOne(company => company.CNPJ == cnpj, CompanyIn);
        public void Delete(Company companyIn)=>_company.DeleteOne(company => company.CNPJ == companyIn.CNPJ);
        public Company Insert(Company company) { _deletedCompany.InsertOne(company); return company; }
    }
}

