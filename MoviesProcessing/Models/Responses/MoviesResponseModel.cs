using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesProcessing.Models
{
   public class MoviesResponseModel
   {      
      [JsonProperty("page")]
      public int Page { get; set; }


      [JsonProperty("total_pages")]
      public int TotalPages { get; set; }

      [JsonProperty("results")]
      public IEnumerable<Movie> Movies { get; set; }
   }
}
