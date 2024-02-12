using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.Authenticate
{
    public class AuthenticateOutput
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
            Title = "Role",
            Description = "Cargo do participante",
            Format = "string"
            )]
        public string Role { get; set; }

        [SwaggerSchema(
            Title = "Id do cliente",
            Description = "Id do cliente ao qual esse participante pertence",
            Format = "long"
            )]
        public long ClientId { get; set; }

        [SwaggerSchema(
            Title = "Token",
            Description = "Bearer token para autenticar o usuário na aplicação",
            Format = "string"
            )]
        public string Token { get; set; }

        [SwaggerSchema(
            Title = "RefreshToken",
            Description = "Token para revalidar o acesso do usuário na aplicação",
            Format = "string"
            )]
        public string RefreshToken { get; set; }
    }
}
