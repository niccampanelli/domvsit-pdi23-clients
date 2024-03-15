using Application.Client.Boundaries.GetAttendantToken;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class GetAttendantTokenCommand : Command<GetAttendantTokenOutput>
    {
        public GetAttendantTokenInput Input { get; set; }

        public GetAttendantTokenCommand(GetAttendantTokenInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetAttendantTokenCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
