using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Models
{
    public class RegisterEmployeeDtoValidator : AbstractValidator<RegisterEmployeesDto>
    {
        public RegisterEmployeeDtoValidator(WarehouseDbContext dbContext)
        {
            
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassword).Equal(p => p.Password);
            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailInUse = dbContext.Employees.Any(e => e.Email == value);
                    if(emailInUse)
                    {
                        context.AddFailure("Email", "That email is taken");
                    }
                });
        }
    }
}
