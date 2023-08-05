using Newtonsoft.Json;

namespace BookStoreTest.Core.GoogleBooks.Models
{
    public class ImageLinks
    {
        [JsonProperty("thumbnail", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Thumbnail { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }
    }
}

