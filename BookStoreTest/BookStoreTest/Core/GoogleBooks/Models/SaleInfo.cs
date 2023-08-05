using Newtonsoft.Json;

namespace BookStoreTest.Core.GoogleBooks.Models
{
    public class SaleInfo
    {
        [JsonProperty("buyLink")]
        public string BuyLink { get; set; }
    }
}

