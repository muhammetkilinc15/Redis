
namespace RedisEcommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        public async Task<List<string>> GetProductsAsync()
        {
            return await Task.FromResult(new List<string> { "Laptop", "Telefon", "Tablet" }); // Dummy data
        }
    }
}
