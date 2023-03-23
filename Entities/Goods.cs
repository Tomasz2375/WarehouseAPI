namespace WarehouseAPI.Entities
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
