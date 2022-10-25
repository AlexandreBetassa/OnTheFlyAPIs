namespace FlightsAPI.DatabaseSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string FlightCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
