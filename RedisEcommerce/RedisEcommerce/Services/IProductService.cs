namespace RedisEcommerce.Services
{
    public interface IProductService
    {
        Task<List<string>> GetProductsAsync();
    }
}
