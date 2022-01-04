using ESourcing.Products.Entities;
using MongoDB.Driver;

namespace ESourcing.Products.Data
{
    public class ProductContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProduct());
            }
        }

        private static IEnumerable<Product> GetConfigureProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name =  "Iphone X",
                    Summary =  "This phone...",
                    Description = "Lorem impus...",
                    Image = "",
                    Price= 950.00M,
                    Category ="SmartPhone"


                },
                 new Product()
                {
                    Name =  "Iphone XS",
                    Summary =  "This phone...",
                    Description = "Lorem impus...",
                    Image = "",
                    Price= 990.00M,
                    Category ="SmartPhone"


                },
                  new Product()
                {
                    Name =  "Iphone 11",
                    Summary =  "This phone...",
                    Description = "Lorem impus...",
                    Image = "",
                    Price= 1000.00M,
                    Category ="SmartPhone"


                }
            };
        }
    }
}
