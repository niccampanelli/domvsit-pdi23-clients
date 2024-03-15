using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.GetAttendantToken
{
    [SwaggerSchema(Required = new string[] { "Id" })]
    public class GetAttendantTokenInput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do cliente",
            Format = "long"
            )]
        public long Id { get; set; }
    }
}
