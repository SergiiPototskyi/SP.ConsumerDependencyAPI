using MassTransit.Transports;
using SP.ConsumerDependencyAPI.Contracts;

namespace SP.ConsumerDependencyAPI
{
    public class ConfigurationCache :
        IReceiveEndpointDependency,
        IConfigurationCache
    {
        readonly TaskCompletionSource<bool> _ready;
        public ConfigurationCache()
        {
            _ready = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        }

        public WarehouseConfiguration? WarehouseConfiguration { get; private set; }

        public Task Ready => _ready.Task;


        public void TrySetConfiguration(WarehouseConfiguration configuration)
        {
            WarehouseConfiguration = configuration;

            _ready.TrySetResult(true);
        }
    }
}
