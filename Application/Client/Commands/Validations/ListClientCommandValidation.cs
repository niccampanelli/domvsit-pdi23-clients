using Application.Client.Boundaries.ListClient;
using FluentValidation;

namespace Application.Client.Commands.Validations
{
    public class ListClientCommandValidation : AbstractValidator<ListClientInput>
    {
        public ListClientCommandValidation()
        {
            RuleFor(i => i.Page)
                .GreaterThan(0).WithMessage("A página deve ser maior que zero");
            RuleFor(i => i.Limit)
                .GreaterThan(0).WithMessage("A quantidade de itens por página deve ser maior que zero");
            RuleFor(i => i.SortOrder)
                .Must(order => order != null ? (new string[] { "ASC", "DESC" }.Contains(order)) : true).WithMessage("A ordem deve ser ou descendente ou ascendente");
            RuleFor(i => i.SortField)
                .Must(field => field != null ? (new string[] { "name", "email", "phone", "createdAt" }.Contains(field)) : true).WithMessage("Campo de ordenação inválido");
        }
    }
}
