namespace WarehouseAPI.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Goods Goods { get; set; }
        public int GoodsId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
