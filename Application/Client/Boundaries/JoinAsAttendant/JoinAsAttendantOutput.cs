using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.JoinAsAttendant
{
    public class JoinAsAttendantOutput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do participante criado",
            Format = "long"
            )]
        public long Id { get; set; }

        [SwaggerSchema(
            Title = "Email no dominio correto",
            Description = "Indica se o email do participante criado é do domínio do cliente",
            Format = "bool"
            )]
        public bool IsEmailInDomain { get; set; }
    }
}
