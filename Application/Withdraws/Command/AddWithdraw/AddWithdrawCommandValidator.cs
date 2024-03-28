using FluentValidation;

namespace Application.Withdraws.Command.AddWithdraw
{
    public class AddWithdrawCommandValidator : AbstractValidator<AddWithdrawCommand>
    {
        public AddWithdrawCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.WithdrawAccount)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.WithdrawMethod)
                .NotEmpty()
                .NotNull();
        }
    }
}