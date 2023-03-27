namespace WarehouseAPI.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateOfBirth { get; set; }

        public EmployeeAddress Address { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
