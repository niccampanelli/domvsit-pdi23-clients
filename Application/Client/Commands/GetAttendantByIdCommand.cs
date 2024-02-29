using Application.Client.Boundaries.GetAttendantById;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class GetAttendantByIdCommand : Command<GetAttendantByIdOutput>
    {
        public GetAttendantByIdInput Input { get; set; }

        public GetAttendantByIdCommand(GetAttendantByIdInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new GetAttendantByIdCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
