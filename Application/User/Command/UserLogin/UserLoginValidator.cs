using FluentValidation;

namespace Application.User.Command.UserLogin
{
    public class UserLoginValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName should be provided and correct!");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}