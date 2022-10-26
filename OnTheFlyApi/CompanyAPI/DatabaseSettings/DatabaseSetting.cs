namespace CompanyAPI.DatabaseSettings
{
    public class DatabaseSetting : IDatabaseSetting
    {
        public string CompanyCollectionName { get; set; }
      //  public string AddressCompany { get; set; }
        public string DeteledCompanyCollectionName { get; set; }
        public string RestrictedCompanyCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
