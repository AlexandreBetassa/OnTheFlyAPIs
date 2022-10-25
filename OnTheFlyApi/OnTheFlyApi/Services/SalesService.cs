using MongoDB.Driver;
using Models;
using SalesAPI.Utils;
using System.Collections.Generic;

namespace SalesAPI.Repositories
{
    public class SalesService
    {
        private readonly IMongoCollection<Sale> _salesService;

        public SalesService(IDatabaseSettings settings)
        {
            var sales = new MongoClient(settings.ConnectionString);
            var database = sales.GetDatabase(settings.DatabaseName);
            _salesService = database.GetCollection<Sale>(settings.SalesConnectionName);
        }

        public Sale Create(Sale sale)
        {
            /*
            Configurar mensageria para post
           */
            return sale;
        }

        public List<Sale> Get() => _salesService.Find(sales => true).ToList();
        public Sale Get(Flights flight) => _salesService.Find(sale => sale.Flight == flight).FirstOrDefault();
        public void Put(Sale saleIn) => _salesService.ReplaceOne(sale => sale.Flight == saleIn.Flight, saleIn);
        //public void Delete(Sale saleIn) => _salesService.DeleteOne(sale);
    }
}
