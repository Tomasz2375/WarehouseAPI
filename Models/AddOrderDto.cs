using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class AddOrderDto
    {
        public DateTime AdmissionDate { get; set; }
        public DateTime RequireDate { get; set; }
    }
}
