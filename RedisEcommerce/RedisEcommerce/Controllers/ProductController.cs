using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisEcommerce.Services;

namespace RedisEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async  Task<IActionResult> GetAsync()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

    }
}
