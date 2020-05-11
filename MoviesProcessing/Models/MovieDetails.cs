using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class MovieDetails : Movie
   {
      [JsonProperty("budget")]
      public long Budget { get; set; }

      [JsonProperty("genres")]
      public IEnumerable<Genre> Genres { get; set; }

      [JsonProperty("production_companies")]
      public IEnumerable<ProductionCompany> Companies { get; set; }

      [JsonProperty("revenue")]
      public long Revenue { get; set; }

      [JsonProperty("runtime")]
      public int Runtime { get; set; }
   }
}
