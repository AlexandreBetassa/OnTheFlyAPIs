namespace CompanyAPI.DatabaseSettings
{
    public interface IDatabaseSettings
    {
        string CompanyCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
