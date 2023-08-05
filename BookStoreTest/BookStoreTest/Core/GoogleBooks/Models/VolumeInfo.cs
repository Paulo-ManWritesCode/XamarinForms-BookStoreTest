using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookStoreTest.Core.GoogleBooks.Models
{
    public class VolumeInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("authors")]
        public List<string> Authors { get; set; }

        [JsonProperty("imageLinks")]
        public ImageLinks ImageLinks { get; set; }
    }
}

