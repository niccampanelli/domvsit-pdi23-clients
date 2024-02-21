using Application.Client.Boundaries.ListAttendant;
using Application.Client.Commands.Validations;
using Application.Commom.Boundaries;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class ListAttendantCommand : Command<PaginatedResponse<ListAttendantOutput>>
    {
        public ListAttendantInput Input { get; set; }

        public ListAttendantCommand(ListAttendantInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new ListAttendantCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
