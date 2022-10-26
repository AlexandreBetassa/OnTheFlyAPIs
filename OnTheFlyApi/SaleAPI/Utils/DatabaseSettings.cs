namespace SaleAPI.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string SalesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
