using Application.Client.Boundaries.GetAttendantById;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class GetAttendantByIdCommandValidation : AbstractValidator<GetAttendantByIdInput>
    {
        public GetAttendantByIdCommandValidation()
        {
            RuleFor(i => i.Id)
                .NotEmpty().WithMessage("O id do participante deve ser informado")
                .NotNull().WithMessage("O id do participante deve ser informado")
                .GreaterThan(0).WithMessage("O id do participante deve ser informado");
        }
    }
}
