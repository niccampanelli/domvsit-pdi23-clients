using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.ListClient
{
    public class ListClientOutput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do cliente",
            Format = "long"
            )]
        public long Id { get; set; }

        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do cliente",
            Format = "string"
            )]
        public string Name { get; set; }

        [SwaggerSchema(
            Title = "Email",
            Description = "Endereço de email do cliente",
            Format = "string"
            )]
        public string Email { get; set; }

        [SwaggerSchema(
            Title = "Telefone",
            Description = "Telefone do cliente",
            Format = "string"
            )]
        public string Phone { get; set; }

        [SwaggerSchema(
            Title = "Id do consultor",
            Description = "Id do consultor associado ao cliente",
            Format = "long"
            )]
        public long ConsultorId { get; set; }

        [SwaggerSchema(
            Title = "Criado em",
            Description = "Data na qual o cliente foi criado",
            Format = "DateTime"
            )]
        public DateTime CreatedAt { get; set; }
    }
}
