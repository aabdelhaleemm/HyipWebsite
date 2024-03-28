using FluentValidation;

namespace Application.User.Command.RequestResetPassword
{
    public class RequestResetPasswordCommandValidator : AbstractValidator<RequestResetPasswordCommand>
    {
        public RequestResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
        }
    }
}