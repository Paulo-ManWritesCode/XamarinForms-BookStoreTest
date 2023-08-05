using BookStoreTest.Core.GoogleBooks.Models;
using Newtonsoft.Json;

namespace BookStoreTest.Core.Books.Models
{
    public class Volume
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; }

        [JsonProperty("saleInfo")]
        public SaleInfo SaleInfo { get; set; }
    }
}

