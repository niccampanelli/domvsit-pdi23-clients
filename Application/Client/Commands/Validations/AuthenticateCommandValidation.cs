using Application.Client.Boundaries.Authenticate;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class AuthenticateCommandValidation : AbstractValidator<AuthenticateInput>
    {
        public AuthenticateCommandValidation()
        {
            RuleFor(i => i.Email)
                .NotEmpty().WithMessage("A string de login deve ser informada")
                .NotNull().WithMessage("A string de login deve ser informada");
            RuleFor(i => i.AttendantToken)
                .NotEmpty().WithMessage("O código de participante deve ser informado")
                .NotNull().WithMessage("A código de participante deve ser informado");
        }
    }
}
