namespace SP.ConsumerDependencyAPI.Contracts
{
    public class GetWarehouseConfiguration
    {
        private int _warehouseId;
        private int _locationNumber;

        public GetWarehouseConfiguration(int warehouseId)
        {
            _warehouseId = warehouseId;
        }

        public int WarehouseId { get { return _warehouseId; } }
        public int LocationNumber { get { return _locationNumber; } }
    }
}
