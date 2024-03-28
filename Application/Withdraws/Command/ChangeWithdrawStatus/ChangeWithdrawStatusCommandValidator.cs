using Domain.Enums;
using FluentValidation;

namespace Application.Withdraws.Command.ChangeWithdrawStatus
{
    public class ChangeWithdrawStatusCommandValidator : AbstractValidator<ChangeWithdrawStatusCommand>
    {
        public ChangeWithdrawStatusCommandValidator()
        {
            RuleFor(x => x.Status)
                .IsEnumName(typeof(Status), false)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.WithdrawId)
                .GreaterThan(0)
                .WithMessage("Invalid Withdraw Request Id");
        }
    }
}