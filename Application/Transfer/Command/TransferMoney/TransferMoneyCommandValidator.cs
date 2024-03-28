using FluentValidation;

namespace Application.Transfer.Command.TransferMoney
{
    public class TransferMoneyCommandValidator : AbstractValidator<TransferMoneyCommand>
    {
        public TransferMoneyCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.RecipientUserName)
                .NotNull()
                .NotEmpty();
        }
    }
}