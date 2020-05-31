using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesProcessing.Models.Responses
{
   public class YouTubeSearchResponse
   {
      [JsonProperty("items")]
      public IEnumerable<YouTubeSearchItem> Items { get; set; }
   }
}
