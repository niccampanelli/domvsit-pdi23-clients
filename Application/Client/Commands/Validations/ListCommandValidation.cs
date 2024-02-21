using Application.Client.Boundaries.List;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class ListCommandValidation : AbstractValidator<ListInput>
    {
        public ListCommandValidation()
        {
            RuleFor(i => i.Page)
                .GreaterThan(0);
            RuleFor(i => i.Limit)
                .GreaterThan(0);
        }
    }
}
