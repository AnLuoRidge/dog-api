using System;
using System.Collections.Generic;

namespace DogApi.Models
{
    public class BreedListResponseModel
    {
        public List<BreedModel> Breeds { get; set; }

        public BreedListResponseModel()
        {
            this.Breeds = new List<BreedModel>();
        }
    }
}
