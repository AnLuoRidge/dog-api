using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DogApi.Models
{
    public class RawBreedModel
    {
        public Dictionary<string, List<string>> Message { get; set; }
        public string Status { get; set; }
    }
}
