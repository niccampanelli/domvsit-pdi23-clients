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
    }
}
