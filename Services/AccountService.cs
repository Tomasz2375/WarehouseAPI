using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WarehouseAPI.Entities;
using WarehouseAPI.Exceptions;
using WarehouseAPI.Models;

namespace WarehouseAPI.Services
{
    public interface IAccountService
    {
        public void RegisterEmployee(RegisterEmployeesDto dto);
        public string GenerateJwt(LoginDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly WarehouseDbContext _dbContext;
        private readonly IPasswordHasher<Employee> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(WarehouseDbContext dbContext, 
            IPasswordHasher<Employee> passwordHasher,
            AuthenticationSettings authenticationSettings)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var employee = _dbContext
                .Employees
                .Include(e => e.Role)
                .FirstOrDefault(e => e.Email == dto.Email);
            if(employee is null)
            {
                throw new BadRequestException("Invalid user name or password");
            }
            var result = _passwordHasher
                .VerifyHashedPassword(employee, employee.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid user name or password");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{employee.Name}, {employee.Surname}"),
                new Claim(ClaimTypes.Role, $"{employee.Role.Name}"),
                new Claim("DateOfBirth", employee.DateOfBirth.ToString("yyyy-MM-dd"))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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
