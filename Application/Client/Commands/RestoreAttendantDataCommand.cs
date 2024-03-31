using Application.Client.Boundaries.RestoreAttendantData;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class RestoreAttendantDataCommand : Command<RestoreAttendantDataOutput>
    {
        public RestoreAttendantDataInput Input { get; set; }

        public RestoreAttendantDataCommand(RestoreAttendantDataInput input)
        {
            Input = input;   
        }

        public override bool IsValid()
        {
            ValidationResult = new RestoreAttendantDataCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
