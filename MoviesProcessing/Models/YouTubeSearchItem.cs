using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class YouTubeSearchItem
   {
      [JsonProperty("id")]
      public YouTubeId Id { get; set; }
   }
}
