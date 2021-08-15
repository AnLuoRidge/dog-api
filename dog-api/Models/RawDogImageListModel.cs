using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace dog_api.Models
{
    public class RawDogImageListModel
    {
        public List<string> Message { get; set; }
        public string Status { get; set; }
    }
}
