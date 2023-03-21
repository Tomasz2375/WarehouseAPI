using Microsoft.AspNetCore.Mvc;
using WarehouseAPI.Entities;
using WarehouseAPI.Models;
using WarehouseAPI.Services;

namespace WarehouseAPI.Controllers
{
    [Route("api/employee")]
    public class EmplyeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmplyeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public ActionResult <IEnumerable<EmployeeDto>> GetAllEmployee()
        {
            var employees = _employeeService.GetAllEmployee();
            return Ok(employees);
        }
        [HttpGet("{id}")]
        public ActionResult <EmployeeDto> GetEmployeeById([FromRoute] int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            return Ok(employee);
        }
    }
}
