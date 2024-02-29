using Application.Client.Boundaries.GetClientByid;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class GetClientByIdCommand : Command<GetClientByIdOutput>
    {
        public GetClientByIdInput Input { get; set; }

        public GetClientByIdCommand(GetClientByIdInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetClientByIdCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
