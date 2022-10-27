using FlightsAPI.DatabaseSettings;
using MongoDB.Driver;
using System.Collections.Generic;
using Models;
using System.Globalization;
using System;

namespace FlightsAPI.Services
{
    public class FlightServices
    {
        private readonly IMongoCollection<Flight> _flights;

        public FlightServices(IDatabaseSettings settings)
        {
            var flight = new MongoClient(settings.ConnectionString);
            var database = flight.GetDatabase(settings.DatabaseName);
            _flights = database.GetCollection<Flight>(settings.FlightCollectionName);
        }

        public Flight Create(Flight flight)
        {
            _flights.InsertOne(flight);
            return flight;
        }

        public List<Flight> Get() => _flights.Find<Flight>(flight => true).ToList();

        public List<Flight> GetByDate(DateTime dateTime) => _flights.Find<Flight>(flight => flight.Departure == dateTime).ToList();

        public Flight GetOne(DateTime dateTime, string rabPlane, string destiny) =>
            _flights.Find<Flight>(flight => flight.Departure == dateTime && flight.Plane.RAB == rabPlane && flight.Destiny.IATA == destiny).FirstOrDefault();

        public void UpdateSales(DateTime dateTime, string rabPlane, string destiny, Flight flightIn) => _flights.ReplaceOne(flight => flight.Departure == dateTime && flight.Plane.RAB == rabPlane && flight.Destiny.IATA == destiny, flightIn);

        public void UpdateStatus(DateTime dateTime, string rabPlane, string destiny, Flight flightIn) => _flights.ReplaceOne(flight => flight.Departure == dateTime && flight.Plane.RAB == rabPlane && flight.Destiny.IATA == destiny, flightIn);

        public void Remove(Flight flightIn) => _flights.DeleteOne(flight => flight.Departure == flightIn.Departure);
    }
}
