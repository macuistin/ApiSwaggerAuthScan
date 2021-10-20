using System;
using System.Collections.Generic;
using System.Net;

namespace ApiSwaggerAuth.Domain.Entities
{
    public class ApiData
    {
        public Uri ApiUrl { get; }
        public ApiStatus Status { get; internal set; }
        public HttpStatusCode ProbeStatus { get; internal set; }
        public IAsyncEnumerable<SwaggerScanResult> SwaggerResults { get; internal set; }

        public ApiData(string apiUrl)
        {
            ApiUrl = new Uri(apiUrl);
        }
    }
}
