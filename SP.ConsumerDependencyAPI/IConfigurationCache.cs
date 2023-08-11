using SP.ConsumerDependencyAPI.Contracts;

namespace SP.ConsumerDependencyAPI
{
    public interface IConfigurationCache
    {
        public WarehouseConfiguration? WarehouseConfiguration { get; }

        public void TrySetConfiguration(WarehouseConfiguration configuration);
    }
}
