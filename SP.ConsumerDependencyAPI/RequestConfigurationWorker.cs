using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SP.ConsumerDependencyAPI.Contracts;

namespace SP.ConsumerDependencyAPI
{
    public class RequestConfigurationWorker :
        BackgroundService
    {
        readonly ConfigurationCache _cache;
        readonly ILogger<RequestConfigurationWorker> _logger;
        readonly IServiceScopeFactory _scopeFactory;

        public RequestConfigurationWorker(ILogger<RequestConfigurationWorker> logger, IServiceScopeFactory scopeFactory, ConfigurationCache cache)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Reqesting Configuration");

            await using var scope = _scopeFactory.CreateAsyncScope();

            var requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<GetWarehouseConfiguration>>();

            var response = await requestClient.GetResponse<WarehouseConfiguration>(new GetWarehouseConfiguration(42),
                stoppingToken);

            _logger.LogInformation("Configuration: WarehouseId: {WarehouseId}, LocationNumber: {LocationNumber}",
                response.Message.WarehouseId, response.Message.LocationNumber);

            _cache.TrySetConfiguration(response.Message);
        }
    }
}
