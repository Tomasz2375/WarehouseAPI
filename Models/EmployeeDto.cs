using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string RoleName { get; set; }
    }
}
