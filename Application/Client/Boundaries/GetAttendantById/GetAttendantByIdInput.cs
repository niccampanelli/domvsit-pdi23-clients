using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.GetAttendantById
{
    [SwaggerSchema(Required = new string[] { "Id" })]
    public class GetAttendantByIdInput
    {
        [SwaggerSchema(
            Title = "Id",
            Description = "Id do participante",
            Format = "long"
            )]
        public long Id { get; set; }
    }
}
