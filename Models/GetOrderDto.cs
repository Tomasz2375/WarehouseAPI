using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? PreparationDate { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime RequireDate { get; set; }
        public List<GetOrderDetailsDto> OrderDetails { get; set; } = new List<GetOrderDetailsDto>();
    }
}
