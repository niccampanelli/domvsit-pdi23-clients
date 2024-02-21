using Application.Client.Boundaries.ListClient;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class ListClientCommandValidation : AbstractValidator<ListClientInput>
    {
        public ListClientCommandValidation()
        {
            RuleFor(i => i.Page)
                .GreaterThan(0);
            RuleFor(i => i.Limit)
                .GreaterThan(0);
        }
    }
}
