using CacheService.Interfaces;
using CacheService.Models;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace CacheService.Dal
{
    public class RedisCache : IRedisCache
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _db;  // Make _db global within the class

        public RedisCache(IConnectionMultiplexer redis, IMetaData metaData)
        {
            _redis = redis;
            _db = redis.GetDatabase();  // Initialize _db in the constructor

        }

        public async Task InsertConnectionStringsAsync(List<ConnectionStringModel> connectionStrings)
        {

            foreach (var connectionString in connectionStrings)
            {
                var cacheKey = $"/connectionstring/{connectionString.Id}";
                var jsonString = JsonSerializer.Serialize(connectionString);

                await _db.StringSetAsync(cacheKey, jsonString);  
            }
        }

        

        public async Task<ConnectionStringModel> GetConnectionStringAsync(int id)
        {
            var cacheKey = $"/connectionstring/{id}";
            var jsonString = await _db.StringGetAsync(cacheKey);

            if (!jsonString.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<ConnectionStringModel>(jsonString);
            }
            return null;

        }

        public async Task<List<ConnectionStringModel>> GetAllConnectionStringsAsync()
        {
            var connectionStrings = new List<ConnectionStringModel>();

            // Define the pattern for your connection string keys
            var server = _redis.GetServer(_redis.GetEndPoints().First());
            var keys = server.Keys(pattern: "/connectionstring/*").ToArray();

            foreach (var key in keys)
            {
                var jsonString = await _db.StringGetAsync(key);

                if (!jsonString.IsNullOrEmpty)
                {
                    var connectionString = JsonSerializer.Deserialize<ConnectionStringModel>(jsonString);
                    connectionStrings.Add(connectionString);
                }
            }

            return connectionStrings;
        }

        public async Task FlushAllDatabasesAsync()
        {
            foreach (var endpoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endpoint);
                if (server.IsConnected)
                {
                    await server.FlushAllDatabasesAsync();
                }
            }
        }
    }
}
