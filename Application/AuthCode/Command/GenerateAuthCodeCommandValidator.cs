using FluentValidation;

namespace Application.AuthCode.Command
{
    public class GenerateAuthCodeCommandValidator : AbstractValidator<GenerateAuthCodeCommand>
    {
        public GenerateAuthCodeCommandValidator()
        {
            RuleFor(x => x.RecipientUserName)
                .NotNull()
                .NotEmpty();
        }
    }
}