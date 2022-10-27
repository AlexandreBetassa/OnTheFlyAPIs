namespace CompanyAPI.DatabaseSettings
{
    public interface IDatabaseSettings
    {
        string CompanyCollectionName { get; set; }
     //   string AddressCompany { get; set; }
        string DeletedCompanyCollectionName { get; set; }
        string RestrictedCompanyCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
