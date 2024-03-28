using FluentValidation;

namespace Application.Admin.Command.LoginAdmin
{
    public class AdminLoginValidator : AbstractValidator<AdminLoginCommand>
    {
        public AdminLoginValidator()
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