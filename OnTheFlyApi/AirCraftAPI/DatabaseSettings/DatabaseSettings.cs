namespace AirCraftAPI.DatabaseSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string AirCraftCollectionName { get; set; }
        public string DeletedAirCraftCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DataBaseName { get; set; }
    }
}
