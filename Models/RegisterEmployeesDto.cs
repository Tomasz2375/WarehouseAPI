using System.ComponentModel.DataAnnotations;

namespace WarehouseAPI.Models
{
    public class RegisterEmployeesDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
