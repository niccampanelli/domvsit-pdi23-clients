using Application.Client.Boundaries.RestoreAttendantData;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class RestoreAttendantDataCommandValidation : AbstractValidator<RestoreAttendantDataInput>
    {
        public RestoreAttendantDataCommandValidation()
        {
            RuleFor(i => i.Authorization)
                .NotEmpty().WithMessage("Participante não autenticado. O token de autenticação precisa estar presente")
                .NotNull().WithMessage("Participante não autenticado. O token de autenticação precisa estar presente")
                .Matches(@"\bBearer\b").WithMessage("Participante não autenticado. Token de autenticação inválido");
        }
    }
}
