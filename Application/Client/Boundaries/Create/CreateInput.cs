using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.Create
{
    [SwaggerSchema(Required = new string[] { "Name", "Email", "Phone", "ConsultorId" })]
    public class CreateInput
    {
        [SwaggerSchema(
            Title = "Nome",
            Description = "Nome do cliente/empresa",
            Format = "string"
            )]
        public string Name { get; set; }

        [SwaggerSchema(
            Title = "Email",
            Description = "Email de contato do cliente/empresa",
            Format = "string"
            )]
        public string Email { get; set; }

        [SwaggerSchema(
            Title = "Telefone",
            Description = "Telefone de contato do cliente/empresa",
            Format = "string"
            )]  
        public string Phone { get; set; }

        [SwaggerSchema(
            Title = "Id consultor",
            Description = "Id do consultor/usuário responsável pelo cadastro do cliente",
            Format = "long"
            )]
        public long ConsultorId { get; set; }
    }
}
