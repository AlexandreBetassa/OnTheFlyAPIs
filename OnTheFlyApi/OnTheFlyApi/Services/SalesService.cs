using MongoDB.Driver;
using Models;

namespace SalesAPI.Repositories
{
    public class SalesService
    {
        private readonly IMongoCollection<Sales> _sales;
    }
}
