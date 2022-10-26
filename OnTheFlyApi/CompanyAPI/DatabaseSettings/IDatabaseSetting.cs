namespace CompanyAPI.DatabaseSettings
{
    public interface IDatabaseSetting
    {
        string CompanyCollectionName { get; set; }
     //   string AddressCompany { get; set; }
        string DeteledCompanyCollectionName { get; set; }
        string RestrictedCompanyCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
