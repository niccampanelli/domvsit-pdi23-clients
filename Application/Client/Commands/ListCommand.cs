using Application.Client.Boundaries.List;
using Application.Client.Commands.Validations;
using Application.Commom.Boundaries;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class ListCommand : Command<PaginatedResponse<ListOutput>>
    {
        public ListInput Input { get; set; }

        public ListCommand(ListInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new ListCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
