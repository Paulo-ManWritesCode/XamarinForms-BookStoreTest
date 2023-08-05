using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookStoreTest.Core.Books.Models
{
    public class GetVolumesResponse
    {
        [JsonProperty("items")]
        public List<Volume> Items { get; set; }
    }
}

