using CompanyAPI.DatabaseSettings;
using Models;
using MongoDB.Driver;

namespace CompanyAPI.Services
{
    public class DeletedCompanyService
    {
        private readonly IMongoCollection<Company> _deletedCompany;

        public DeletedCompanyService(IDatabaseSettings settings)
        {
            var deletedCompany = new MongoClient(settings.ConnectionString);
            var database = deletedCompany.GetDatabase(settings.DatabaseName);
            _deletedCompany = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public Company Create(Company company) { _deletedCompany.InsertOne(company); return company; }
    }
}
