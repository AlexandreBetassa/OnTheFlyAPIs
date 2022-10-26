namespace PassengerAPI.Utils
{
    public interface IDatabaseSettings
    {
        string PassengerCollectionName { get; set; }
        string DeletedPassengerCollectionName { get; set; }
        string RestrictedPassengerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
