using FluentValidation;

namespace Application.User.Command.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}