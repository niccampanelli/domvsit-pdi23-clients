using Application.Client.Boundaries.JoinAsAttendant;
using Application.Client.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class JoinAsAttendantCommand : Command<JoinAsAttendantOutput>
    {
        public JoinAsAttendantInput Input { get; set; }

        public JoinAsAttendantCommand(JoinAsAttendantInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new JoinAsAttendantCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
