using System;
using System.Collections.Generic;

namespace dog_api.Models
{
    public class BreedModel
    {
        public string Breed { get; set; }
        public List<string> Subbreeds { get; set; }
    }
}
