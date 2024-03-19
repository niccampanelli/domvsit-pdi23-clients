using Application.Client.Boundaries.UpdateClient;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class UpdateClientCommandValidation : AbstractValidator<UpdateClientInput>
    {
        public UpdateClientCommandValidation()
        {
            RuleFor(i => i.Email)
                .EmailAddress().WithMessage("Informe um endereço de email válido");
        }
    }
}
