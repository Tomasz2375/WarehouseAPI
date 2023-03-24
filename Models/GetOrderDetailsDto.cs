using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class GetOrderDetailsDto
    {
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public string GoodsName { get; set; }
        public int Quantity { get; set; }
    }
}
