using CommonExtensions.Dal;

namespace IndexingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MySqlDal _mySqlDal;

        public Worker(ILogger<Worker> logger,MySqlDal sqlDal)
        {
            _logger = logger;
            _mySqlDal = sqlDal;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cacheEndpoint = _mySqlDal.GetEndpointById(1);
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
