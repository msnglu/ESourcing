using ESourcing.Products.Settings.Interface;

namespace ESourcing.Products.Settings
{
    public class ProductDatabaseSettings : IProductDatabaseSettings
    {
        public string ConnectionString { get ; set; } = null!;
        public string DatabaseName { get; set ; } = null!;
        public string CollectionName { get ; set; } = null!;
    }
}
