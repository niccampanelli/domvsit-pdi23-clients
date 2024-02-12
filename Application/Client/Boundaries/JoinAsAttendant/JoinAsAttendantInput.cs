using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.JoinAsAttendant
{
    [SwaggerSchema(Required = new string[] { "Name", "Email", "Role", "AttendantToken" })]
    public class JoinAsAttendantInput
    {
        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do participante",
            Format = "string"
            )]
        public string Name { get; set; }

        [SwaggerSchema(
            Title = "Email",
            Description = "Email do participante",
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
            Title = "Token de participante",
            Description = "Token de acesso ao cliente",
            Format = "string"
            )]
        public string AttendantToken { get; set; }
    }
}
