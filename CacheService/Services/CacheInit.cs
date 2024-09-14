using CacheService.Dal;
using CacheService.Interfaces;
using Microsoft.Extensions.Hosting;


namespace CacheService.Services
{
    public class CacheInit : IHostedService
    {
        private readonly IMetaData _metaData;
        private readonly IRedisCache _redisCache;
        public CacheInit(IMetaData metaData, IRedisCache redis)
        {
            _metaData = metaData;
            _redisCache = redis;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            DataconnectionInit();

            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void DataconnectionInit()
        {
            var connections = _metaData.GetConnections();

            _redisCache.InsertConnectionStringsAsync(connections);
        }
    }
}
