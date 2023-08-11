using MassTransit;
using SP.ConsumerDependencyAPI.Contracts;

namespace SP.ConsumerDependencyAPI.Components
{
    public class InquiryConsumer : IConsumer<GetWarehouseLocationNumber>
    {
        readonly IConfigurationCache _cache;

        public InquiryConsumer(IConfigurationCache cache)
        {
            _cache = _cache;
        }

        public Task Consume(ConsumeContext<GetWarehouseLocationNumber> context)
        {
            return context.RespondAsync(new WarehouseLocationNumber(_cache.WarehouseConfiguration.WarehouseId,
                _cache.WarehouseConfiguration.LocationNumber));
        }
    }
}
