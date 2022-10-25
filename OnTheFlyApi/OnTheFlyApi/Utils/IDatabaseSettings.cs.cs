namespace SalesAPI.Utils
{
    public interface IDatabaseSettings
    {
        public string SalesConnectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
