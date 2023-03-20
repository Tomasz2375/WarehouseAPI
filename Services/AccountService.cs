using Microsoft.AspNetCore.Identity;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IAccountService
    {
        public void RegisterEmployee(RegisterEmployeesDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        public AccountService(WarehouseDbContext dbContext, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }
        public void RegisterEmployee(RegisterEmployeesDto dto)
        {
            var newEmployee = new Employee
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newEmployee, dto.Password);
            newEmployee.PasswordHash = hashedPassword;
            _dbContext.Add(newEmployee);
            _dbContext.SaveChanges();
        }
    }
}
