using System.Text.Json.Serialization;

namespace ApiSwaggerAuth.Domain.Entities
{
    public class SwaggerUrlEntry
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
