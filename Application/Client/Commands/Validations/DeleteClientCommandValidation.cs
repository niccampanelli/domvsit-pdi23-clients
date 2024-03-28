using Application.Client.Boundaries.DeleteClient;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class DeleteClientCommandValidation : AbstractValidator<DeleteClientInput>
    {
        public DeleteClientCommandValidation()
        {
            RuleFor(i => i.Id)
                .NotEmpty().WithMessage("O id do cliente deve ser informado")
                .NotNull().WithMessage("O id do cliente deve ser informado")
                .GreaterThan(0).WithMessage("O id do cliente deve ser informado");
        }
    }
}
