using Models;
using MongoDB.Driver;
using SaleAPI.Utils;
using System.Collections.Generic;

namespace SaleAPI.Services
{
    public class SaleService
    {
        private readonly IMongoCollection<Sale> _salesService;

        public SaleService(IDatabaseSettings settings)
        {
            var sales = new MongoClient(settings.ConnectionString);
            var database = sales.GetDatabase(settings.DatabaseName);
            _salesService = database.GetCollection<Sale>(settings.SalesConnectionName);
        }

        public Sale Create(Sale sale)
        {
            _salesService.InsertOne(sale);
            return sale;
        }

        public List<Sale> Get() => _salesService.Find(sales => true).ToList();
        public Sale Get(Sale saleIn) => _salesService.Find(sale => sale.Flight.Destiny == saleIn.Flight.Destiny &&
        sale.Flight.Departure == saleIn.Flight.Departure).FirstOrDefault();
        public void Delete(Sale saleIn) => _salesService.DeleteOne(sale => sale.Flight.Destiny == saleIn.Flight.Destiny &&
        sale.Flight.Departure == saleIn.Flight.Departure);
    }
}
