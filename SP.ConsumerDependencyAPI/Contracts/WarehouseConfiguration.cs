namespace SP.ConsumerDependencyAPI.Contracts
{
    public class WarehouseConfiguration
    {
        private int _warehouseId;
        private string _locationName;
        private string __locationNumber;

        public WarehouseConfiguration(int warehouseId, string locationName, string locationNumber)
        {
            _warehouseId = warehouseId;
            _locationName = locationName;
            __locationNumber = locationNumber;
        }

        public int WarehouseId { get; set; }

        public string LocationName { get; set; }

        public string LocationNumber { get; set; }
    }
}
