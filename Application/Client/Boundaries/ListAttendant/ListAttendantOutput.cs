using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.ListAttendant
{
    public class ListAttendantOutput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do participante",
            Format = "long"
            )]
        public long Id { get; set; }

        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do participante",
            Format = "string"
            )]
        public string Name { get; set; }

        [SwaggerSchema(
            Title = "Email",
            Description = "Endereço de email do participante",
            Format = "string"
            )]
        public string Email { get; set; }

        [SwaggerSchema(
            Title = "Cargo",
            Description = "Cargo do participante",
            Format = "string"
            )]
        public string Role { get; set; }

        [SwaggerSchema(
            Title = "Id do cliente",
            Description = "Id do cliente associado ao participante",
            Format = "long"
            )]
        public long ClientId { get; set; }

        [SwaggerSchema(
            Title = "Criado em",
            Description = "Data na qual o evento foi criado",
            Format = "DateTime"
            )]
        public DateTime CreatedAt { get; set; }
    }
}
