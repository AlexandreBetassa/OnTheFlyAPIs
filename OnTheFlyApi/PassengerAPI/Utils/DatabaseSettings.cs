using static PassengerAPI.Utils.DatabaseSettings;

namespace PassengerAPI.Utils
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string PassengerCollectionName { get; set; }
        public string DeletedPassengerCollectionName { get; set; }
        public string RestrictedPassengerCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
