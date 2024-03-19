using Application.Client.Boundaries.UpdateClient;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class UpdateClientCommand : Command<UpdateClientOutput>
    {
        public UpdateClientInput Input { get; set; }

        public UpdateClientCommand(UpdateClientInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateClientCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
