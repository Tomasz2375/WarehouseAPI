using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Models
{
    public class AddGoodsDto
    {
        [Required]
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
