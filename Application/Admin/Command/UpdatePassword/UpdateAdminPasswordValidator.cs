using FluentValidation;

namespace Application.Admin.Command.UpdatePassword
{
    public class UpdateAdminPasswordValidator : AbstractValidator<UpdateAdminPasswordCommand>
    {
        public UpdateAdminPasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5);
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .NotNull()
                .MinimumLength(5);
        }
    }
}