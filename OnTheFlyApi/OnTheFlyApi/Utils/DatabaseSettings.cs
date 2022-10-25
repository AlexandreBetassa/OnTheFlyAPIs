namespace SalesAPI.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string SalesConnectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
