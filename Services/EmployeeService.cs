using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Exceptions;

namespace WarehouseAPI.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployee();
        EmployeeDto GetEmployeeById(int id);
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
        public EmployeeDto GetEmployeeById(int id)
        {
            var employee = _dbContext.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Id == id);

            if(employee is null)
            {
                throw new NotFoundException("Employee not found");
            }
            
            var result = _mapper.Map<EmployeeDto>(employee);
            return result;
        }
    }
}
