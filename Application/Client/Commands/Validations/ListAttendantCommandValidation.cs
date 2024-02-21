using Application.Client.Boundaries.ListAttendant;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class ListAttendantCommandValidation : AbstractValidator<ListAttendantInput>
    {
        public ListAttendantCommandValidation()
        {
            RuleFor(i => i.Page)
                .GreaterThan(0);
            RuleFor(i => i.Limit)
                .GreaterThan(0);
        }
    }
}
