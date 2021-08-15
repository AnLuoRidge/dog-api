using System;
using System.Collections.Generic;

namespace DogApi.Models
{
    public class BreedModel
    {
        public string Breed { get; set; }
        public List<string> Subbreeds { get; set; }
    }
}
