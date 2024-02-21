using Application.Commom.Boundaries;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.ListClient
{
    public class ListClientInput : IPaginationRequest, ISortingRequest
    {
        [SwaggerSchema(
            Title = "Página",
            Description = "Página a ser listada",
            Format = "int"
            )]
        public int? Page { get; set; }

        [SwaggerSchema(
            Title = "Limite",
            Description = "Limite de itens na página",
            Format = "int"
            )]
        public int? Limit { get; set; }

        [SwaggerSchema(
            Title = "Campo",
            Description = "Campo para ordenar",
            Format = "string"
            )]
        public string? SortField { get; set; }

        [SwaggerSchema(
            Title = "Ordem",
            Description = "Direção da ordenação (asc, desc)",
            Format = "string"
            )]
        public string? SortOrder { get; set; }

        [SwaggerSchema(
            Title = "Id do consultor",
            Description = "Id do consultor associado ao cliente",
            Format = "long"
            )]
        public long? ConsultorId { get; set; }

        [SwaggerSchema(
            Title = "Pesquisa",
            Description = "Texto para pesquisar entre os atributos do cliente",
            Format = "string"
            )]
        public string? Search { get; set; }
    }
}
