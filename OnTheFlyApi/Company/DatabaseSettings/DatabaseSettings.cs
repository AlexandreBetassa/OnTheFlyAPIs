namespace CompanyAPI.DatabaseSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
       public string CompanyCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
