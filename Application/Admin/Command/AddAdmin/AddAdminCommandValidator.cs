using FluentValidation;

namespace Application.Admin.Command.AddAdmin
{
    public class AddAdminCommandValidator : AbstractValidator<AddAdminCommand>
    {
        public AddAdminCommandValidator()
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