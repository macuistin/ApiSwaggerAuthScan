using System.Net;

namespace ApiSwaggerAuth.Domain.Entities
{
    public class SwaggerProbeResult
    {
        public string Path { get; init; }
        public string Verb { get; init; }
        public HttpStatusCode StatusCode { get; init; }
    }
}
