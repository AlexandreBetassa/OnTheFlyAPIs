using AirCraftAPI.DatabaseSettings;
using Models;
using MongoDB.Driver;

namespace AirCraftAPI.Services
{
    public class DeletedAirCraftService
    {
        private readonly IMongoCollection<AirCraft> _deletedAircraft;
        
        public DeletedAirCraftService(IDatabaseSettings settings)
        {
            var deletedAircraft = new MongoClient(settings.ConnectionString);
            var database = deletedAircraft.GetDatabase(settings.DataBaseName);
            _deletedAircraft = database.GetCollection<AirCraft>(settings.DeletedAirCraftCollectionName);
        }

        public AirCraft Insert(AirCraft aircraftDelete)
        {
            _deletedAircraft.InsertOne(aircraftDelete);
            return aircraftDelete;
        }




    }
}
