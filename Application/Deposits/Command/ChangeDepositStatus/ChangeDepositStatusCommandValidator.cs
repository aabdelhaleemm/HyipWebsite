using Domain.Enums;
using FluentValidation;

namespace Application.Deposits.Command.ChangeDepositStatus
{
    public class ChangeDepositStatusCommandValidator : AbstractValidator<ChangeDepositStatusCommand>
    {
        public ChangeDepositStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsEnumName(typeof(Status),false)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.DepositId)
                .GreaterThan(0)
                .WithMessage("Invalid Deposit Request Id");
        }
    }
}