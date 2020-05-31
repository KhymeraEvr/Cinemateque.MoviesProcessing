using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class YouTubeId
   {
      [JsonProperty("videoId")]
      public string Id { get; set; }
   }
}
