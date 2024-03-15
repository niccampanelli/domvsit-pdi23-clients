using Application.Client.Boundaries.GetAttendantToken;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class GetAttendantTokenCommandValidation : AbstractValidator<GetAttendantTokenInput>
    {
        public GetAttendantTokenCommandValidation()
        {
            RuleFor(i => i.Id)
                .NotEmpty().WithMessage("O id do cliente deve ser informado")
                .NotNull().WithMessage("O id do cliente deve ser informado")
                .GreaterThan(0).WithMessage("O id do cliente deve ser informado");
        }
    }
}
