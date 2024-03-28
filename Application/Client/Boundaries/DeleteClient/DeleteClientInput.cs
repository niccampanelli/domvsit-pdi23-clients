using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.DeleteClient
{
    [SwaggerSchema(Required = new string[] { "Id" })]
    public class DeleteClientInput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do cliente",
            Format = "long"
            )]
        public long Id { get; set; }
    }
}
