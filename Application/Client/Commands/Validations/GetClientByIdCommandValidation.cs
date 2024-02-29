using Application.Client.Boundaries.GetClientByid;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class GetClientByIdCommandValidation : AbstractValidator<GetClientByIdInput>
    {
        public GetClientByIdCommandValidation()
        {
            RuleFor(i => i.Id)
                .NotEmpty().WithMessage("O id do cliente deve ser informado")
                .NotNull().WithMessage("O id do cliente deve ser informado")
                .GreaterThan(0).WithMessage("O id do cliente deve ser informado");
        }
    }
}
