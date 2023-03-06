namespace WarehouseAPI.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
