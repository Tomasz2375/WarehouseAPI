using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? PreparationDate { get; set; }
        public DateTime? PostDate { get; set; }
        public DateTime RequireDate { get; set; }
        public String Status { get; set; }
    }
}
