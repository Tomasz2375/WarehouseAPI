using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployee();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeService(WarehouseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<EmployeeDto> GetAllEmployee()
        {
            var employees = _dbContext.Employees
                .Include(e => e.Role)
                .ToList();
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return result;
        }
    }
}
