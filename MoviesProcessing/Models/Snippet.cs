using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class Snippet
   {
      [JsonProperty("title")]
      public string Title { get; set; }

      [JsonProperty("description")]
      public string Description { get; set; }

      [JsonProperty("publishedAt")]
      public string PublishDate { get; set; }
   }
}
