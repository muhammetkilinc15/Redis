namespace RedisEcommerce.Repository
{
    public interface IProductRepository
    {
        Task<List<string>> GetProductsAsync();
    }
}
