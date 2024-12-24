using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("set/{value}")]
        public IActionResult SetValue(string value)
        {
            _memoryCache.Set("cachedValue", value);
            return Ok("Value set successfully");
        }

        [HttpGet("get")]
        public IActionResult GetValue()
        {
            var value = _memoryCache.Get<string>("cachedValue");
            if (value == null)
            {
                return NotFound("Value not found");
            }
            return Ok(value);
        }

        [HttpDelete("remove")]
        public IActionResult RemoveValue()
        {
            _memoryCache.Remove("cachedValue");
            return Ok("Value removed successfully");
        }

        [HttpGet("tryget")]
        public IActionResult TryGetValue()
        {
            if (_memoryCache.TryGetValue("cachedValue", out string value))
            {
                return Ok(value);
            }
            return NotFound("Value not found");
        }

        [HttpGet("setdate")]
        public IActionResult SetDate()
        {
            _memoryCache.Set("date",DateTime.Now,options: new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30), // mutlak ömrü 30 saniye
                SlidingExpiration= TimeSpan.FromSeconds(5) // kayıt üzerinde işlem yapılmadığı sürece 10 saniye sonra silinir
                
            });

            return Ok("Date set successfully");
        }

        [HttpGet("getdate")]
        public IActionResult GetDate()
        {
            return Ok(_memoryCache.Get("date"));
        }
    }
}