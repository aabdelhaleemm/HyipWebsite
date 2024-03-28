using FluentValidation;

namespace Application.Deposits.Command.AddDeposit
{
    public class AddDepositCommandValidator : AbstractValidator<AddDepositCommand>
    {
        public AddDepositCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(1)
                .NotNull()
                .WithMessage("Amount should be provided!");
            RuleFor(x => x.File)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.OperationId)
                .NotEmpty()
                .NotNull();
        }
    }
}