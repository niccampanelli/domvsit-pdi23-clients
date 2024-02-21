using Application.Client.Boundaries.ListClient;
using Application.Client.Commands.Validations;
using Application.Commom.Boundaries;
using Domain.Base.Messages;

namespace Application.Client.Commands
{
    public class ListClientCommand : Command<PaginatedResponse<ListClientOutput>>
    {
        public ListClientInput Input { get; set; }

        public ListClientCommand(ListClientInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new ListClientCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
