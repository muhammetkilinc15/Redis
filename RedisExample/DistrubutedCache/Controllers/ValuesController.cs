using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace DistrubutedCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _cache;
        public ValuesController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("set")]
        public async Task<IActionResult> Set(string name, string surname)
        {
            await _cache.SetStringAsync("name", name,options:new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(7)
            });
            await _cache.SetAsync("surname", Encoding.UTF8.GetBytes(surname),options:new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(7)
            });
            return Ok();
        }
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var name = await _cache.GetStringAsync("name");
            var surname = await _cache.GetAsync("surname");
            if(name == null || surname == null)
                return NotFound();

            return Ok(new { name, surname = Encoding.UTF8.GetString(surname) });
        }
    }
}
