using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.GetClientByid
{
    [SwaggerSchema(Required = new string[] { "Id" })]
    public class GetClientByIdInput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do cliente",
            Format = "long"
            )]
        public long Id { get; set; }
    }
}
