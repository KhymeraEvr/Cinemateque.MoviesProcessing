using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class Statistics
   {
      [JsonProperty("viewCount")]
      public string ViewCount { get; set; }

      [JsonProperty("likeCount")]
      public string LikeCount { get; set; }

      [JsonProperty("dislikeCount")]
      public string DislikeCount { get; set; }

      [JsonProperty("commentCount")]
      public string CommentCount { get; set; }

   }
}
