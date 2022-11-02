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

        public AirCraft Create(AirCraft aircraftInsert)
        {
            _aircraft.InsertOne(aircraftInsert);
            return aircraftInsert;
        }

        public List<AirCraft> GetAll() => _aircraft.Find<AirCraft>(aircraft => true).ToList();

        public List<AirCraft> GetAllByCnpj(string companyCnpj) => _aircraft.Find<AirCraft>(aircraft => aircraft.Company.CNPJ == companyCnpj).ToList();

        public AirCraft GetOneByRAB(string rab) => _aircraft.Find<AirCraft>(aircraft => aircraft.RAB == rab).FirstOrDefault();

        public void Update(AirCraft aircraftUpdate)
        {
            _aircraft.ReplaceOne(aircraft => aircraft.RAB == aircraftUpdate.RAB, aircraftUpdate);
        }

        public void UpdateCapacity(AirCraft aircraftUpdate, string rab)
        {
            _aircraft.ReplaceOne(aircraft => aircraft.RAB == rab, aircraftUpdate);
        }
        public void Remove(AirCraft aircraftRemove) => _aircraft.DeleteOne(aircraft => aircraft.RAB == aircraftRemove.RAB);
    }
}
