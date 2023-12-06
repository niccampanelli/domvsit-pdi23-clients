using Application.Client.Boundaries.Create;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class CreateCommandValidation : AbstractValidator<CreateInput>
    {
        public CreateCommandValidation()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("O nome deve ser informado")
                .NotNull().WithMessage("O nome deve ser informado");
            RuleFor(i => i.Email)
                .NotEmpty().WithMessage("O endereço de email deve ser informado")
                .NotNull().WithMessage("O endereço de email deve ser informado")
                .EmailAddress().WithMessage("Informe um endereço de email válido");
            RuleFor(i => i.Phone)
                .NotEmpty().WithMessage("O telefone deve ser informado")
                .NotNull().WithMessage("O telefone deve ser informado")
                .MinimumLength(10).WithMessage("Informe um telefone válido com DDD")
                .MaximumLength(11).WithMessage("Informe um telefone válido com DDD");
            RuleFor(i => i.ConsultorId)
                .NotEmpty().WithMessage("O id do consultor deve ser informado")
                .NotNull().WithMessage("O id do consultor deve ser informado")
                .GreaterThan(0).WithMessage("Informe um id do consultor válido");
        }
    }
}
