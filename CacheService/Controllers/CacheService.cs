using CacheService.Interfaces;
using CacheService.Models;
using CommonExtensions.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CacheService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheService:ControllerBase
    {
        private readonly IRedisCache _redis;

        public CacheService(IRedisCache redis)
        {
            _redis = redis;
        }

        [HttpDelete("clear")]
        public async Task<ActionResult> ClearCache()
        {
           await _redis.FlushAllDatabasesAsync();
           return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServerResponse<ConnectionStringModel>>> GetConnectionString(int id)
        {
            var response = new ServerResponse<ConnectionStringModel>();
            var connectionString = await _redis.GetConnectionStringAsync(id);
            if (connectionString == null)
            {
                response.Success = false;
                response.Message = "Id doesnt Exists";
                return NotFound(response);
            }
            return Ok(response.Data=connectionString);
            
        }

        [HttpGet]
        public async Task<ActionResult<ServerResponse<List<ConnectionStringModel>>>> GetAllConectionStrings()
        {
            var response = new ServerResponse<List<ConnectionStringModel>>();

            var result = await _redis.GetAllConnectionStringsAsync();
            if (result == null)
            {
                response.Success = false;
                return NotFound(response);
            }
            return Ok(response.Data=result);
        }
        
    }
}
