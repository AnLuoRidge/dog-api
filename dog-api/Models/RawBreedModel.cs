using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dog_api.Models
{
    public class RawBreedModel
    {
        [JsonPropertyName("message")]
        public Dictionary<string, List<string>> Message { get; set; }
    }
}
