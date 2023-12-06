using Application.Client.Boundaries.Create;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class CreateCommand : Command<CreateOutput>
    {
        public CreateInput Input { get; set; }

        public CreateCommand(CreateInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
