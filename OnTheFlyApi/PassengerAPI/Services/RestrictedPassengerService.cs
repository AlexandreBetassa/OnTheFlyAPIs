using Models;
using MongoDB.Driver;
using PassengerAPI.Utils;
using System.Collections.Generic;

namespace PassengerAPI.Services
{
    public class RestrictedPassengerService
    {
        private readonly IMongoCollection<RestrictedPassenger> _restrictedPassengers;

        public RestrictedPassengerService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _restrictedPassengers = database.GetCollection<RestrictedPassenger>(settings.RestrictedPassengerCollectionName);
        }

        public RestrictedPassenger Create(RestrictedPassenger restrictedPassenger)
        {
            _restrictedPassengers.InsertOne(restrictedPassenger);
            return restrictedPassenger;
        }
        public List<RestrictedPassenger> Get() => _restrictedPassengers.Find<RestrictedPassenger>(restrictedPassenger => true).ToList();
        public RestrictedPassenger Get(string cpf) => _restrictedPassengers.Find<RestrictedPassenger>(restrictedPassenger => restrictedPassenger.CPF == cpf).FirstOrDefault();
        public RestrictedPassenger Remove(string cpf)
        {
            var restrictedPassenger = Get(cpf);
            if (restrictedPassenger == null) return null;
            _restrictedPassengers.DeleteOne(restrictedPassenger => restrictedPassenger.CPF == cpf);
            return restrictedPassenger;
        }
        
    }
}
