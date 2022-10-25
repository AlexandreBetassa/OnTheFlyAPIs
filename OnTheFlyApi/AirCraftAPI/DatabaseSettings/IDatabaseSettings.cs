namespace AirCraftAPI.DatabaseSettings
{
    public interface IDatabaseSettings
    {
        string AirCraftCollectionName { get; set; }
        string DeletedAirCraftCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
    }
}
