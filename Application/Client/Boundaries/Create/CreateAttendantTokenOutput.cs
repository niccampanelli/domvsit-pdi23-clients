using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.Create
{
    public class CreateAttendantTokenOutput
    {
        [SwaggerSchema(
            Title = "Id do token de participante",
            Description = "Id do token de acesso dos colaboradores do cliente",
            Format = "long"
            )]
        public long Id { get; set; }

        [SwaggerSchema(
            Title = "Token",
            Description = "Token de acesso dos colaboradores do cliente",
            Format = "string"
            )]
        public string Value { get; set; }

        [SwaggerSchema(
            Title = "Data de criação",
            Description = "Data de criação do token de acesso dos colaboradores do cliente",
            Format = "DateTime"
            )]
        public DateTime CreatedAt { get; set; }

        [SwaggerSchema(
            Title = "Data de expiração",
            Description = "Data de expiração do token de acesso dos colaboradores do cliente",
            Format = "DateTime"
            )]
        public DateTime ExpiresAt { get; set; }
    }
}
