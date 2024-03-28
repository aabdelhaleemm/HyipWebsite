using FluentValidation;

namespace Application.User.Command.AddUser
{
    public class AddUserCommandValidation : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email Address is required for registration! ");
            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();
        }
    }
}