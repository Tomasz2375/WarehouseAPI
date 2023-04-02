using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Exceptions;

namespace WarehouseAPI.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDto GetEmployeeById(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(WarehouseDbContext dbContext, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            _logger.LogInformation("Invoke: GetAllEmployees()");

            var employees = _dbContext.Employees
                .Include(e => e.Role)
                .ToList();
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return result;
        }

        public EmployeeDto GetEmployeeById(int id)
        {
            _logger.LogInformation($"Invoke: GetEmployeeById(id = {id})");

            var employee = _dbContext.Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Id == id);

            if(employee is null)
            {
                _logger.LogError($"Error: employee with Id = {id} not found");
                throw new NotFoundException("Employee not found");
            }

            _logger.LogInformation($"Get employee with id = {id}");

            var result = _mapper.Map<EmployeeDto>(employee);
            return result;
        }
    }
}
