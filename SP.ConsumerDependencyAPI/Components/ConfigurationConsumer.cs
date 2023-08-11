using MassTransit;
using SP.ConsumerDependencyAPI.Contracts;

namespace SP.ConsumerDependencyAPI.Components
{
    public class ConfigurationConsumer :
        IConsumer<GetWarehouseConfiguration>
    {
        public async Task Consume(ConsumeContext<GetWarehouseConfiguration> context)
        {
            // whoa this database is super slow today
            await Task.Delay(8000);

            await context.RespondAsync(new WarehouseConfiguration(42, "Southside", "8675309"));
        }
    }
}
