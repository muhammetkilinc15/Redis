using RedisEcommerce.Repository;
using RedisEcommerce.Services.Redis;

namespace RedisEcommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly ICacheService _cacheService;
        private readonly IProductRepository _repository;

        public ProductService(ICacheService cacheService, IProductRepository repository)
        {
            _cacheService = cacheService;
            _repository = repository;
        }

        public async Task<List<string>> GetProductsAsync()
        {
            const string cacheKey = "product_list";

            // Önbellekte veriyi kontrol et
            var cachedProducts = await _cacheService.GetAsync<List<string>>(cacheKey);
            if (cachedProducts != null)
            {
                return cachedProducts;
            }

            // Eğer önbellekte yoksa veritabanından çek
            var products = await _repository.GetProductsAsync();

            // Veriyi önbelleğe kaydet
            await _cacheService.SetAsync(cacheKey, products, TimeSpan.FromSeconds(10));

            return products;
        }
    }
}
