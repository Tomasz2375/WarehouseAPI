namespace WarehouseAPI.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
