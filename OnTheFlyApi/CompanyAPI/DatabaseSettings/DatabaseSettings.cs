﻿namespace CompanyAPI.DatabaseSettings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string CompanyCollectionName { get; set; }
      //  public string AddressCompany { get; set; }
        public string DeletedCompanyCollectionName { get; set; }
        public string RestrictedCompanyCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
