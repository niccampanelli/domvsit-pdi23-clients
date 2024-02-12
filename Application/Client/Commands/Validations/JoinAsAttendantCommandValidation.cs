using Application.Client.Boundaries.JoinAsAttendant;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class JoinAsAttendantCommandValidation : AbstractValidator<JoinAsAttendantInput>
    {
        public JoinAsAttendantCommandValidation()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("O nome deve ser informado")
                .NotNull().WithMessage("O nome deve ser informado");
            RuleFor(i => i.Email)
                .NotEmpty().WithMessage("O endereço de email deve ser informado")
                .NotNull().WithMessage("O endereço de email deve ser informado")
                .EmailAddress().WithMessage("Informe um endereço de email válido");
            RuleFor(i => i.Role)
                .NotEmpty().WithMessage("O cargo deve ser informado")
                .NotNull().WithMessage("O cargo deve ser informado");
            RuleFor(i => i.AttendantToken)
                .NotEmpty().WithMessage("O token de participante deve ser informado")
                .NotNull().WithMessage("O token de participante deve ser informado");
        }
    }
}
