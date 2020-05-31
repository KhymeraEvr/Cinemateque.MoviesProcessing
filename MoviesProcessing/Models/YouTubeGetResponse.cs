using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class YouTubeGetResponse
   {
      [JsonProperty("items")]
      public IEnumerable<YouTubeGetItem> Items { get; set; }
   }
}
