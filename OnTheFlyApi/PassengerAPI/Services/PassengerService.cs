using MongoDB.Driver;
using PassengerAPI.Utils;
using System.Collections.Generic;
using Models;

namespace PassengerAPI.Services
{
    public class PassengerService
    {
        private readonly IMongoCollection<Passenger> _passengers;

        public PassengerService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _passengers = database.GetCollection<Passenger>(settings.PassengerCollectionName);
        }

        public Passenger Create(Passenger passenger)
        {
            _passengers.InsertOne(passenger);
            return passenger;
        }

        public List<Passenger> Get() => _passengers.Find<Passenger>(passenger => true).ToList();
        public Passenger Get(string id) => _passengers.Find<Passenger>(passenger => passenger.CPF == id).FirstOrDefault();
        public void Update(string id, Passenger passengerIn) => _passengers.ReplaceOne(passenger => passenger.CPF == id, passengerIn);
        public void Remove(Passenger passengerIn) => _passengers.DeleteOne(passenger => passenger.CPF == passengerIn.CPF);
    }
}
