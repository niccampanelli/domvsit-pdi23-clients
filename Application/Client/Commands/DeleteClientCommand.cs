using Application.Client.Boundaries.DeleteClient;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class DeleteClientCommand : Command<DeleteClientOutput>
    {
        public DeleteClientInput Input { get; set; }

        public DeleteClientCommand(DeleteClientInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteClientCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
