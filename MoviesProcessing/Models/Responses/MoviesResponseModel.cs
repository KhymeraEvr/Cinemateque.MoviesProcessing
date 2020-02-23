using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesProcessing.Models
{
   public class MoviesResponseModel
   {
      [JsonProperty("results")]
      public IEnumerable<Movie> Movies { get; set; }
   }
}
