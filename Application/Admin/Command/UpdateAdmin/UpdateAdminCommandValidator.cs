using FluentValidation;

namespace Application.Admin.Command.UpdateAdmin
{
    public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
    {
        public UpdateAdminCommandValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(5)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Email Address is required for registration! ");
        }
    }
}