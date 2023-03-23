using System.ComponentModel.DataAnnotations;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class AddOrderDetailsDto
    {
        public int Quantity { get; set; }
        public int GoodsId { get; set; }
    }
}
