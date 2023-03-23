using FluentValidation;
using WarehouseAPI.Entities;

namespace WarehouseAPI.Models.Validators
{
    public class AddOrderDetailsDtoValidation : AbstractValidator<AddOrderDetailsDto>
    {
        public AddOrderDetailsDtoValidation(WarehouseDbContext dbContext)
        {
            RuleFor(o => o.Quantity).NotEmpty();
            RuleFor(o => o.GoodsId).NotEmpty();
            RuleFor(o => o.GoodsId).Custom((value, context) =>
            {
                var goodExist = dbContext.Goods.Any(g => g.Id == value);
                if(!goodExist)
                {
                    context.AddFailure("GoodsId", "This goods don't exist");
                }
            });
        }
    }
}
