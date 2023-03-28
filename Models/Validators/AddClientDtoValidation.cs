using FluentValidation;

namespace WarehouseAPI.Models.Validators
{
    public class AddClientDtoValidation : AbstractValidator<AddClientDto>
    {
        public AddClientDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(9);
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.HouseNumber).NotEmpty();
        }
    }
}
