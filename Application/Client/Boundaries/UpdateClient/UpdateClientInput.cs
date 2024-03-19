using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.UpdateClient
{
    public class UpdateClientInput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do cliente/empresa",
            Format = "long"
            )]
        public long Id { get; set; }

        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do cliente/empresa",
            Format = "string"
            )]
        public string? Name { get; set; }

        [SwaggerSchema(
            Title = "Email",
            Description = "Email de contato do cliente/empresa",
            Format = "string"
            )]
        public string? Email { get; set; }

        [SwaggerSchema(
            Title = "Telefone",
            Description = "Telefone de contato do cliente/empresa",
            Format = "string"
            )]
        public string? Phone { get; set; }
    }
}
