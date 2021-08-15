using System;
using System.Collections.Generic;

namespace dog_api.Models
{
    public class DogImageListModel
    {
        public List<string> Images { get; set; }
        public int Total { get; set; }

        public DogImageListModel()
        {
            this.Images = new List<string>();
        }
    }
}
