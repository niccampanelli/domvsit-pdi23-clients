using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.Authenticate
{
    [SwaggerSchema(Required = new string[] { "Email", "AttendantToken" })]
    public class AuthenticateInput
    {
        [SwaggerSchema(
            Title = "Email",
            Description = "Endereço de email do participante",
            Format = "string"
            )]
        public string Email { get; set; }

        [SwaggerSchema(
            Title = "Código de participante",
            Description = "Código de participante fornecido pelo cliente",
            Format = "string"
            )]
        public string AttendantToken { get; set; }
    }
}
