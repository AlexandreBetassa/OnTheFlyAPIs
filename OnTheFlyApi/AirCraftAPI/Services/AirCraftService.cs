using MongoDB.Driver;
using Models;
using AirCraftAPI.DatabaseSettings;
using System.Collections.Generic;

namespace AirCraftAPI.Services
{
    public class AirCraftService
    {
        private readonly IMongoCollection<AirCraft> _aircraft;

        public AirCraftService(IDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DataBaseName);
            _aircraft = database.GetCollection<AirCraft>(settings.AirCraftCollectionName);
        }

        
        public List<AirCraft> Get() => _aircraft.Find<AirCraft>(aircraft => true).ToList();

        public AirCraft Get(string rab) => _aircraft.Find<AirCraft>(aircraft => aircraft.Rab == rab).FirstOrDefault();
    }
}
