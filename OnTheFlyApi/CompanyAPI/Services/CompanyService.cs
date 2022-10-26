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

        public CompanyService(IDatabaseSetting setting)
        {
            var company = new MongoClient(setting.ConnectionString);
            var database = company.GetDatabase(setting.DatabaseName);
            _companies = database.GetCollection<Company>(setting.CompanyCollectionName);
        }

        public Company Create(Company company){ _companies.InsertOne(company); return company; }
        public List<Company> GetAll() => _companies.Find<Company>(company => true).ToList();
        public Company GetOneCNPJ(string cnpj) => _companies.Find(company => company.CNPJ == cnpj).FirstOrDefault();
        public void Update(string id, Company CompanyIn) => _companies.ReplaceOne(person => person.Id == id, CompanyIn);
        public void Delete(Company companyIn)=>_companies.DeleteOne(company => company.CNPJ == companyIn.CNPJ);
    }
}

