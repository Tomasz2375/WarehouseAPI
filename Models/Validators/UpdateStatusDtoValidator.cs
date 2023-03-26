using FluentValidation;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Models.Validators
{
    public class UpdateStatusDtoValidator : AbstractValidator<UpdateStatusDto>
    {
        public UpdateStatusDtoValidator(WarehouseDbContext dbContext)
        {
            RuleFor(s => s.StatusId).NotEmpty();
            RuleFor(s => s.StatusId).Custom((value, context) =>
            {
                var isCorrectValue = dbContext.Status.Any(s => s.Id == value);
                if (!isCorrectValue) 
                {
                    context.AddFailure("This status id do not exist in database");
                }
            });
        }
    }
}
