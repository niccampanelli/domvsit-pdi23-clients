using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.Create
{
    public class CreateOutput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do cliente criado",
            Format = "long"
            )]
        public long CreatedId { get; set; }

        [SwaggerSchema(
            Title = "Id de participante",
            Description = "Id do participante administrador criado",
            Format = "long"
            )]
        public long CreatedAttendantId { get; set; }

        [SwaggerSchema(
            Title = "Token de participante",
            Description = "Token utilizado para autorizar os colaboradores do cliente",
            Format = "CreateAttendantTokenOutput"
            )]
        public CreateAttendantTokenOutput AttendantToken { get; set; }
    }
}
