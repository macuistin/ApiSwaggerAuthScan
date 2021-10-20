using System.Collections.Generic;

namespace ApiSwaggerAuth.Domain.Entities
{
    public class SwaggerScanResult
    {
        public string Url { get; set; }
        public string Name { get; internal set; }
        public IAsyncEnumerable<SwaggerProbeResult> MethodProbeResults { get; set; }
    }
}
