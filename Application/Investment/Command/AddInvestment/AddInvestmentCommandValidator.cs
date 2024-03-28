using Domain.Enums;
using FluentValidation;

namespace Application.Investment.Command.AddInvestment
{
    public class AddInvestmentCommandValidator : AbstractValidator<AddInvestmentCommand>
    {
        public AddInvestmentCommandValidator()
        {
            RuleFor(x => x.Plan)
                .IsEnumName(typeof(InvestmentsPlan))
                .NotEmpty()
                .NotNull();


        }
    }
}