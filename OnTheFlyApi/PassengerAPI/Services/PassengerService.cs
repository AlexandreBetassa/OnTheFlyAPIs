using MongoDB.Driver;
using PassengerAPI.Utils;
using System.Collections.Generic;
using Models;

namespace PassengerAPI.Services
{
    public class PassengerService
    {
        private readonly IMongoCollection<Passenger> _passengers;
        private readonly IMongoCollection<Passenger> _deletedPassengers;

        public PassengerService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _passengers = database.GetCollection<Passenger>(settings.PassengerCollectionName);
            _deletedPassengers = database.GetCollection<Passenger>(settings.DeletedPassengerCollectionName);
        }

        public Passenger Create(Passenger passenger)
        {
            _passengers.InsertOne(passenger);
            return passenger;
        }

        public List<Passenger> Get() => _passengers.Find<Passenger>(passenger => true).ToList();
        public Passenger Get(string cpf) => _passengers.Find<Passenger>(passenger => passenger.CPF == cpf).FirstOrDefault();
        public Passenger Replace(string cpf, Passenger passengerIn)
        {
            var passenger = Get(cpf);
            if (passenger != null) return null;

            _passengers.ReplaceOne(passenger => passenger.CPF == cpf, passengerIn);
            return passengerIn;
        }

        public Passenger Remove(string cpf)
        {
            var passenger = Get(cpf);
            if (passenger == null) return null;

            _passengers.DeleteOne(passenger => passenger.CPF == cpf);
            _deletedPassengers.InsertOne(passenger);
            
            return passenger;
        }
    }
}
