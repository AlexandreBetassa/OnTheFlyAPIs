using AirportAPI.Models;
using AirportAPI.Utils;
using MongoDB.Driver;
using System.Collections.Generic;

namespace AirportAPI.Serivces
{
    public class AirportService
    {
        private readonly IMongoCollection<Airport> _airports;

        public AirportService(IDataBaseSettings settings)
        {
            var airport = new MongoClient(settings.ConnectionString);
            var database = airport.GetDatabase(settings.DataBaseName);
            _airports = database.GetCollection<Airport>(settings.AirportCollectionName);
        }

        //public Airport Create (Airport airport)
        //{
        //    _airports.InsertOne(airport);
        //    return airport;
        //}
        public List<Airport> Get() =>
            _airports.Find(airport => true).ToList();

        public Airport Get(string iata) =>
            _airports.Find<Airport>(airport => airport.iata == iata).FirstOrDefault();

        public List<Airport> GetByIcao(string icao) =>
            _airports.Find<Airport>(airport => airport.icao == icao).ToList();

        public List<Airport> GetByCountry(string country_id) =>
            _airports.Find<Airport>(airport => airport.country_id == country_id).ToList();

        public List<Airport> GetByCity(string city_code) =>
            _airports.Find<Airport>(airport => airport.city_code == city_code).ToList();
    }
}