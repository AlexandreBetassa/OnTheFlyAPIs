using System.Collections.Generic;
using CompanyAPI.DatabaseSettings;
using Models;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    public class CompanyService
    {
        private readonly IMongoCollection<Company> _companies;
        //private readonly IMongoCollection<Address> _address;

        public CompanyService(IDatabaseSettings settings)
        {
            var company = new MongoClient(settings.ConnectionString);
            var database = company.GetDatabase(settings.DatabaseName);
            _companies = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public Company Create(Company company){ _companies.InsertOne(company); return company; }
        public List<Company> GetAll() => _companies.Find<Company>(company => true).ToList();
        public Company GetOneCNPJ(string cnpj) => _companies.Find<Company>(company => company.CNPJ == cnpj).FirstOrDefault();
        public void Update(string id, Company CompanyIn) => _companies.ReplaceOne(person => person.Id == id, CompanyIn);
        public void Delete(Company companyIn)=>_companies.DeleteOne(company => company.CNPJ == companyIn.CNPJ);
    }
}

