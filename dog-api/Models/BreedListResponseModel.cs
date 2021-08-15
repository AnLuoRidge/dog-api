using System;
using System.Collections.Generic;

namespace dog_api.Models
{
    public class BreedListResponse
    {
        public List<BreedModel> Breeds { get; set; }

        public BreedListResponse()
        {
            this.Breeds = new List<BreedModel>();
        }
    }
}
