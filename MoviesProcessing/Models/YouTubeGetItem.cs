using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class YouTubeGetItem
   {
      [JsonProperty("id")]
      public string Id { get; set; }

      [JsonProperty("statistics")]
      public Statistics Statistics { get; set; }

      [JsonProperty("snippet")]
      public Snippet Snippet { get; set; }
   }
}
