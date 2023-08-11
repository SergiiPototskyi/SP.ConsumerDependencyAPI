namespace SP.ConsumerDependencyAPI.Contracts
{
    public class WarehouseLocationNumber
    {
        private int _warehouseId;
        private string _locationNumber;

        public WarehouseLocationNumber(int warehouseId, string locationNumber)
        {
            _warehouseId = warehouseId;
            _locationNumber = locationNumber;
        }

        public int WarehouseId { get { return _warehouseId; } }
        public string LocationNumber { get { return _locationNumber; } }
    }
}
