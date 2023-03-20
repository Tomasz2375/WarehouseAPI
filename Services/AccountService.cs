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
        public AccountService(WarehouseDbContext dbContext)
        {
            _dbContext = dbContext;
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
            _dbContext.Add(newEmployee);
            _dbContext.SaveChanges();
        }
    }
}
