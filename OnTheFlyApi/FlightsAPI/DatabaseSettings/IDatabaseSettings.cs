namespace FlightsAPI.DatabaseSettings
{
    public interface IDatabaseSettings
    {
        string FlightCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
