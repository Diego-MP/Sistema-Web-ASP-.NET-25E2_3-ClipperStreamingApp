using System.Text.Json.Serialization;

namespace ClipperStreamingApp.WebApp.Models;

public class ApiResponse
    {
        public class PlanosApiResponse
        {
            [JsonPropertyName("$values")]
            public List<PlanoViewModel> Values { get; set; }
        }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
