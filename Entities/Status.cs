namespace WarehouseAPI.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
