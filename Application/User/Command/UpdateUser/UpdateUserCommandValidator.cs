using FluentValidation;

namespace Application.User.Command.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(5)
                .NotNull()
                .NotEmpty();
        }
    }
}