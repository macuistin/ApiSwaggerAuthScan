using System.Net.Http;
using Microsoft.OpenApi.Models;

namespace ApiSwaggerAuth.Domain.Extensions
{
    public static class OpenApiExtensions
    {
        public static HttpMethod ToHttpMethod(this OperationType operationType)
        {
            return operationType switch
            {
                OperationType.Get => HttpMethod.Get,
                OperationType.Put => HttpMethod.Put,
                OperationType.Post => HttpMethod.Post,
                OperationType.Delete => HttpMethod.Delete,
                OperationType.Options => HttpMethod.Options,
                OperationType.Head => HttpMethod.Head,
                OperationType.Patch => HttpMethod.Patch,
                OperationType.Trace => HttpMethod.Trace,
                _ => HttpMethod.Get,
            };
        }

    }
}
