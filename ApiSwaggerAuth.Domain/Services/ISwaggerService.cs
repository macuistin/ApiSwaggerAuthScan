using System;
using System.Collections.Generic;
using ApiSwaggerAuth.Domain.Entities;
using Microsoft.OpenApi.Readers;

namespace ApiSwaggerAuth.Domain.Services
{
    public interface ISwaggerService
    {
        OpenApiDiagnostic Diagnostic { get; }

        IAsyncEnumerable<SwaggerScanResult> ScanAsync(Uri apiUrl);
    }
}