using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Client.Boundaries.RestoreAttendantData
{
    public class RestoreAttendantDataInput
    {
        [SwaggerSchema(
            Title = "Token de autorização",
            Description = "Token bearer JWT para autorizar o participante",
            Format = "string"
            )]
        [FromHeader]
        public string Authorization { get; set; }
    }
}
