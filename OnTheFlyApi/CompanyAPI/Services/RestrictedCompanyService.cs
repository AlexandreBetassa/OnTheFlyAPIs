using System.Collections.Generic;
using CompanyAPI.DatabaseSettings;
using Models;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    public class RestrictedCompanyService
    {
        private readonly IMongoCollection<RestrictedCompany> _restritedCompany;

        public RestrictedCompanyService(IDatabaseSettings settings)
        {
            var restritedCompany = new MongoClient(settings.ConnectionString);
            var database = restritedCompany.GetDatabase(settings.DatabaseName);
            _restritedCompany = database.GetCollection<RestrictedCompany>(settings.RestrictedCompanyCollectionName);
        }

        public RestrictedCompany Create(RestrictedCompany restritedCompany) { _restritedCompany.InsertOne(restritedCompany); return restritedCompany; }
        public List<RestrictedCompany> GetAll() => _restritedCompany.Find<RestrictedCompany>(company => true).ToList();

        public RestrictedCompany GetOneCNPJ(string cnpj) => _restritedCompany.Find<RestrictedCompany>(restritedCompany => restritedCompany.CNPJ == cnpj).FirstOrDefault();
        public void Delete(RestrictedCompany RestritedCpmpanyIn) => _restritedCompany.DeleteOne(company => company.CNPJ == RestritedCpmpanyIn.CNPJ);

    }
}
