using ESourcing.Sourcing.Settings.Interface;

namespace ESourcing.Sourcing.Settings
{
    public class SourcingDatabaseSettings : ISourcingDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
